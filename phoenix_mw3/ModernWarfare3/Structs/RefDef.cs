using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare3.Structs
{
	// This is a severely stripped down version of Refdef.
	[StructLayout(LayoutKind.Sequential)]
	internal struct RefDef
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		private byte[] _0x0000;
		public Int32 Width;
		public Int32 Height;
		public float FovX;
		public float FovY;
		public Vector3D OriginVec;
		public Vector3D ViewAxis1;
		public Vector3D ViewAxis2;
		public Vector3D ViewAxis3;
	}
}
