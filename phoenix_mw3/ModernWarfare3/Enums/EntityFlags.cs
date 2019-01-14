using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.ModernWarfare3.Enums
{
	[Flags]
	internal enum EntityFlags
	{
		EF_STANDING = 0x00000000,
		EF_CROUCHED = 0x00000004,
		EF_PRONE = 0x00000008,
		EF_MENU_OPEN = 0x00000100,
		EF_DEAD = 0x00040000,
		EF_SCOPED = 0x00080000,
		EF_FIRING = 0x00800000
	}
}
