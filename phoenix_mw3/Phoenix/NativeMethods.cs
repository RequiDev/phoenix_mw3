using Phoenix.Structs;
using System;
using System.Runtime.InteropServices;

namespace Phoenix
{
    internal class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int key);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
