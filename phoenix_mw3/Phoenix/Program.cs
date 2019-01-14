using Phoenix.CommandSystem;
using Phoenix.ConsoleSystem;
using Phoenix.MemorySystem;
using Phoenix.ThreadingSystem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Phoenix.Structs;
using Phoenix.Features;
using Phoenix.Extensions;
using Phoenix.Overlay;
using System.Drawing;
using Phoenix.ModernWarfare3;

namespace Phoenix
{
    internal class Program
    {
        private static ProcessMemory Memory => Phoenix.Memory;
		private static OverlayWindow Overlay => Phoenix.Overlay;

		private static void Main(string[] args)
        {
            CommandHandler.Setup();

            ThreadManager.Add(new ThreadFunction("CommandHandler", CommandHandler.Worker));
            ThreadManager.Add(new ThreadFunction("MainLoop", MainLoop));
			ThreadManager.Add(new ThreadFunction("DrawingLoop", DrawingLoop));

            AttachToGame();
        }

		private static void DrawingLoop()
		{
			Phoenix.Overlay = new Overlay.OverlayWindow(Memory.MainWindowHandle, false);

			Overlay.Show();


			while (Memory.IsProcessRunning)
			{
				//Thread.Sleep(0);

				Overlay.Graphics.BeginScene();
				Overlay.Graphics.ClearScene();

				if (Native.GetForegroundWindow() != Overlay.ParentWindow)
				{
					Overlay.Graphics.EndScene();
					continue;
				}

				ESP.Run();

				Overlay.Graphics.EndScene();
			}
		}

        private static void MainLoop()
		{
			while (Memory.IsProcessRunning)
			{
                Thread.Sleep(1);
                EntityBase.Update();
            }
        }

        private static void AttachToGame()
        {
            Process process;
            Console.WriteNotification($"\n  Looking for {Phoenix.GameName}...");
            while (Memory == null)
            {
                Thread.Sleep(100);
                try
                {
                    process = Process.GetProcessesByName(Phoenix.ProcessName).FirstOrDefault(p => p.Threads.Count > 0);
                    if (process == null) continue;
                }
                catch
                {
                    continue;
                }

                Phoenix.Memory = new ProcessMemory(process);
            }

            Console.WriteNotification("\n  Found and attached to it!");
            CommandHandler.Load();
            Console.WriteCommandLine();

			ThreadManager.RunAll();
        }
    }
}
