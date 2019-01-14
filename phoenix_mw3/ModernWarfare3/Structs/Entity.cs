using Phoenix.ModernWarfare3.Enums;
using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare3.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Entity
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		private byte[] _0x0000; //0x0000
		public short IsValid; //0x0002 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private byte[] _0x0004; //0x0004
		public Vector3D Origin; //0x0014 
		public Vector3D Angles; //0x0020 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
		private byte[] _0x002C; //0x002C 
		public EntityFlags Flags; //0x0068
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		private byte[] _0x006C; //0x006C 
		public Vector3D OldOrigin; //0x0078 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
		public byte[] _0x0084;
		public Vector3D OldAngles; //0x009c
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		private byte[] _0x00A8; //0x0084 
		public Int32 ClientNum; //0x00D0 
		public EntityType Type; //0x00D4 - type enum
		private byte _0x00D8; //0x00D8 
		public byte InMenu; //0x00D9 
		public byte WeaponFlags; //0x00DA 8 = zoom, 128 = shoot, 136 = zoomshoot
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
		private byte[] _0x00DB; //0x00DB
		public Vector3D NewOrigin; //0x00E8 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
		private byte[] _0x00F4; //0x00F4
		public Vector3D NewAngles; //0x010C 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		private byte[] _0x0118; //0x0118
		public byte Weapon2; //0x0134 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 99)]
		private byte[] _0x0135; //0x0135
		public byte WeaponID; //0x0198 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		private byte[] _0x0199; // 0x0199
		public Int32 IsGrenadeLauncher; //0x019C 
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		private byte[] _0x01A0; // 0x01A0
		public Int32 IsAlive; // 0x1D0
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
		private byte[] _0x01D4; //0x01D4
	}
}
