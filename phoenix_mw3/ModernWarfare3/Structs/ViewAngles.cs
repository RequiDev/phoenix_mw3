using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare3.Structs
{
	// This is actually a part of Refdef (Refdef ViewAngles) but I've seperated it
	// to avoid reading the whole Refdef struct to get the view angles.
	[StructLayout(LayoutKind.Sequential)]
	internal struct ViewAngles
	{
		public Vector3D Angle;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		private byte[] _0x000C;
		public Vector3D Origin;
	}
}
