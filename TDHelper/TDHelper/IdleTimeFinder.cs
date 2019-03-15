using System;
using System.Runtime.InteropServices;

namespace TDHelper
{
    class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            var lastInput = new LASTINPUTINFO();

            lastInput.cbSize = (uint)Marshal.SizeOf(lastInput);

            GetLastInputInfo(ref lastInput);

            return (uint)Environment.TickCount - lastInput.dwTime;
        }

        public static long GetLastInputTime()
        {
            var lastInput = new LASTINPUTINFO();

            lastInput.cbSize = (uint)Marshal.SizeOf(lastInput);

            if (!GetLastInputInfo(ref lastInput))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInput.dwTime;
        }

        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
    }
}
