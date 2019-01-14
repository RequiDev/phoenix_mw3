using Phoenix.ConsoleSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Phoenix.CommandSystem
{
    internal static class CommandHandler
    {
        public static List<Command> Commands = new List<Command>();
        public static void Worker()
        {
            while (Phoenix.Memory.IsProcessRunning)
            {
                var fullCommand = Console.ReadLine();
                var commandArray = fullCommand.ToLower().Split(' ');
                var command = commandArray[0];
                var param = commandArray.Length > 1 ? commandArray[1] : "";
                var value = commandArray.Length > 2 ? commandArray[2] : "";
                HandleCommand(command, param, value);
                Console.WriteCommandLine();
            }
        }

        public static void Setup()
        {
            Console.Title = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteWatermark();

            Commands.Add(new Command("esp", "ESP"));
            //Commands.Add(new Command("aimbot", "Aimbot"));

            AddParameter("esp", "active", "0", "Wether esp is active or not.");

            //AddParameter("aimbot", "fov", "1", "Field of view radius for the aimbot.");
            //AddParameter("aimbot", "smooth", "0", "How much smooth will be applied to the aimbot.");
            //AddParameter("aimbot", "bone", "14", "On which bone the aimbot will aim.");
            //AddParameter("aimbot", "key", "1", "Key to press to activate the aimbot.");
            //AddParameter("aimbot", "visible", "0", "Basic visible check for the aimbot");
            //AddParameter("aimbot", "norecoil", "1", "Recoil compensation for the aimbot.");
        }

        private static void AddParameter(string command, string parameter, string defaultValue, string desc = "This is a basic parameter")
        {
            GetCommand(command).Parameters.Add(new CommandParameter(parameter, new CommandParameterValue(defaultValue), desc));
        }

        private static void HandleCommand(string command, string parameter, string value)
        {
            switch (command)
            {
                case "load":
                    Load(parameter);
                    break;
                case "save":
                    Save(parameter);
                    break;
                case "help":
                    DisplayHelp();
                    break;
				case "rank":
					GiveRank80();
					break;
				case "prestige":
					GivePrestige(parameter);
					break;
				default:
                    var cmd = GetCommand(command);
                    if (!cmd)
                    {
                        Console.WriteSuccess($"  Could not find command '{command}'.", false);
                        return;
                    }
                    if (parameter == "")
                    {
                        DisplayParameters(cmd);
                        return;
                    }
                    var param = GetParameter(command, parameter);
                    if (!param)
                    {
                        Console.WriteSuccess($"  Could not find parameter '{parameter}' in command '{command}'.", false);
                        return;
                    }
                    if (value == "")
                    {
                        Console.WriteNotification($"  - {cmd.Name} {param.Name} ({param.Description})\n    Current value of '{command} {parameter}' is {GetParameter(command, parameter).Value}\n");
                        return;
                    }
                    param.Value = new CommandParameterValue(value);
                    if (param.Value.ToFloat() < 0.0f)
                    {
                        Console.WriteSuccess($"  Value has to be convertable to a digit", false);
                        return;
                    }
                    Console.WriteNotification($"  Set value of '{command} {parameter}' to '{value}'.");
                    break;
            }
        }

        public static void Save(string file = "./settings.ini")
        {
            if (string.IsNullOrEmpty(file)) file = "./settings.ini";
            if (!file.EndsWith(".ini")) file += ".ini";
            if (!file.StartsWith("./")) file = "./" + file;
            foreach (var cmd in Commands)
            {
                foreach(var param in cmd.Parameters)
                {
                    WriteValue(cmd.Name, param.Name, param.Value.Value, file);
                }
            }
            //SaveSkins();
            Console.WriteNotification($"  Saved Settings to {file.Replace("./", "")}!");
        }

        public static void Load(string file = "./settings.ini")
        {
            if (string.IsNullOrEmpty(file)) file = "./settings.ini";
            if (!file.EndsWith(".ini")) file += ".ini";
            if (!file.StartsWith("./")) file = "./" + file;
            if (!File.Exists(file))
            {
                Console.WriteSuccess($"  {file.Replace("./", "")} does not exist. Did not change anything.\n", false);
                return;
            }
            foreach (var cmd in Commands)
            {
                foreach (var param in cmd.Parameters)
                {
                    param.Value.Value = ParseInteger(ReadValue(cmd.Name, param.Name, file), param.Value.ToInt32()).ToString();
                }
            }
            //LoadSkins();
            Console.WriteNotification($"  Loaded Settings from {file.Replace("./", "")}!\n");
        }

        private static void DisplayParameters(Command cmd)
        {
            Console.WriteNotification($"  {cmd.Name} ({cmd.Description})");
            cmd.Parameters.ForEach(delegate (CommandParameter param)
            {
                Console.WriteNotification($"    - {cmd.Name} {param.Name} ({param.Description})");
            });
        }

        private static void DisplayHelp()
        {
            Commands.ForEach(delegate (Command pCmd)
            {
                Console.WriteNotification($"  - {pCmd.Name} ({pCmd.Description})");
            });
        }

		private static void GiveRank80()
		{
			Phoenix.Memory.Write<int>((System.IntPtr)0x1CE0E38, 1746200);
		}

		private static void GivePrestige(string value)
		{
			int intVal;
			int.TryParse(value, out intVal);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1CE1048, intVal);
		}

		private static Command GetCommand(string command)
        {
            return Commands.FirstOrDefault(com => com.Name == command);

        }
        public static CommandParameter GetParameter(string command, string parameter)
        {
            return GetCommand(command).Parameters.FirstOrDefault(param => param.Name == parameter);
        }

        #region ReadWrite
        public static void WriteValue(string section, string key, string value, string File = ".\\settings.ini")
        {
            WritePrivateProfileString(section, key, value, File);
        }

        public static string ReadValue(string section, string key, string File = ".\\settings.ini")
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, File);

            return temp.ToString();
        }
        #endregion
        #region Parsing
        public static bool ParseBoolean(string input, bool defaultVal = false)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            bool output;

            if (!bool.TryParse(input, out output))
                return defaultVal;

            return output;
        }

        public static int ParseInteger(string input, int defaultVal = 0)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            int output;

            if (!int.TryParse(input, out output))
                return defaultVal;

            return output;
        }

        public static float ParseFloat(string input, float defaultVal = 0.0f)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            float output;

            if (!float.TryParse(input, out output))
                return defaultVal;

            return output;
        }
        #endregion
        #region Native
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
    }
}
