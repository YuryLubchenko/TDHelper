using System;
using System.Runtime.InteropServices;

namespace Services
{
    public static class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LastInputInfo plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static (int X, int Y) GetMousePosition()
        {
            if (GetCursorPos(out Point point))
            {
                return (point.X, point.Y);
            }
            return (0, 0);
        }        

        public static TimeSpan GetIdleTime()
        {
            var lastInput = new LastInputInfo();

            lastInput.CbSize = (uint)Marshal.SizeOf(lastInput);

            GetLastInputInfo(ref lastInput);

            return TimeSpan.FromMilliseconds(Environment.TickCount - lastInput.DwTime);
        }

        public static long GetLastInputTime()
        {
            var lastInput = new LastInputInfo();

            lastInput.CbSize = (uint)Marshal.SizeOf(lastInput);

            if (!GetLastInputInfo(ref lastInput))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInput.DwTime;
        }

        private struct LastInputInfo
        {
            public uint CbSize;
            public uint DwTime;
        }

        private struct Point
        {
            public int X;
            public int Y;
        }
    }
}
