using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare3.Structs
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal struct ClientInfo
	{
		public Int32 Valid; // 0x0
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		private byte[] _0x0004; // 0x0004
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string Name; // 0x000C 
		public Int32 Team; // 0x001C 
		public Int32 Team2; // 0x0020 
		public Int32 Rank; // 0x0024
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private byte[] _0x0028; // 0x0028
		public Int32 Perk; // 0x0038 Perk & 0x20 == BlindEye
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		private byte[] _0x003C; // 0x003C
		public Int32 Score; // 0x0044
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1096)]
		private byte[] _0x0048; // 0x0048
		public byte IsStanding; // 0x0490
		public byte IsWalking; // 0x0491
		public byte IsSprinting; // 0x0492
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
		private byte[] _0x0493; // 0x0493
		public byte IsShooting; // 0x04A0
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		private byte[] _0x04A1; // 0x04A1
		public byte IsZoomed; // 0x04A8
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 71)]
		private byte[] _0x04A9; // 0x04A9
		public Int32 WeaponNum1; // 0x04F0
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
		private byte[] _0x04F4; // 0x04F4
		public Int32 WeaponNum2; // 0x050C
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 84)]
		private byte[] _0x0510; // 0x0510
	}
}
