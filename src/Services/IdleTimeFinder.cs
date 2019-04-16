using System;
using System.Runtime.InteropServices;

namespace Services
{
    public static class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static (int X, int Y) GetMousePosition()
        {
            if (GetCursorPos(out POINT point))
            {
                return (point.X, point.Y);
            }
            return (0, 0);
        }        

        public static TimeSpan GetIdleTime()
        {
            var lastInput = new LASTINPUTINFO();

            lastInput.cbSize = (uint)Marshal.SizeOf(lastInput);

            GetLastInputInfo(ref lastInput);

            return TimeSpan.FromMilliseconds(Environment.TickCount - lastInput.dwTime);
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

        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        private struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
