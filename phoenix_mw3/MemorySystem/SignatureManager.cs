using System;
using System.Diagnostics;
using System.Text;

namespace Phoenix.MemorySystem
{
    internal static class SignatureManager
    {
        private static ProcessMemory Memory => Phoenix.Memory;

        public static IntPtr GetViewAngle()
        {
			return (IntPtr)0x96CCDC;
		}

        public static IntPtr GetEntityList()
        {
			return (IntPtr)0xA065C0;
		}

        public static IntPtr GetWorldToViewMatrix()
        {
			return (IntPtr)0x9681B0;
		}

        public static IntPtr GetLocalIndex()
        {
			return (IntPtr)0x8FCFA0;
        }
    }
}
