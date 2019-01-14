namespace Phoenix.CommandSystem
{
    internal class CommandParameter
    {
        public string Name { get; set; }
        public CommandParameterValue Value { get; set; }
        public string Description { get; set; }

        public CommandParameter(string name, CommandParameterValue value, string desc = "This is a basic parameter")
        {
            Name = name;
            Description = desc;
            Value = value;
        }

        public static bool operator !(CommandParameter cmdParam)
        {
            return cmdParam == null;
        }
    }
}
