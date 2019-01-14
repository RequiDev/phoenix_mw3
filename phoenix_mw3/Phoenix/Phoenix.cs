using Phoenix.MemorySystem;
using Phoenix.ModernWarfare3;
using Phoenix.Overlay;
using System;
using System.Collections.Generic;

namespace Phoenix
{
	internal class Phoenix
	{
		public static string GameName { get { return "Call of Duty®: Modern Warfare® 3 Multiplayer"; } }
		public static string ProcessName { get { return "iw5mp"; } }
		public static OverlayWindow Overlay { get; set; }
        public static EntityList EntityList { get; set; }
        public static ProcessMemory Memory { get; set; }
    }
}
