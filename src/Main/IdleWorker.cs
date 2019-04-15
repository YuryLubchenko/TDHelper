using System;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Main
{
    class IdleWorker
    {
        private (int X, int Y) LastMousePosition { get; set; }
        private Timer Timer { get; }

        private const int TimerInterval = 1000;

        private static readonly TimeSpan IdleThreshold = TimeSpan.FromSeconds(55);

        private InputSimulator InputSimulator { get; }

        public IdleWorker()
        {
            LastMousePosition = IdleTimeFinder.GetMousePosition();
            InputSimulator = new InputSimulator();

            Timer = new Timer(Callback, null, TimerInterval, TimerInterval);

            Console.WriteLine($"Started: {DateTime.Now}");
        }

        private bool CheckPosition()
        {
            var mPos = IdleTimeFinder.GetMousePosition();
            var lastPos = LastMousePosition;
            LastMousePosition = mPos;

            return (mPos.X != lastPos.X || mPos.Y != lastPos.Y);
        }

        private async void Callback(object state)
        {
            if (CheckPosition()) return;

            var idleTime = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());

            if (idleTime < IdleThreshold)
                return;

            var random = new Random();

            for (var i = 0; i < random.Next(20, 60); i++)
            {
                switch (random.Next(3))
                {
                    case 0: InputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL); break;
                    case 1: InputSimulator.Mouse.XButtonClick(0); InputSimulator.Mouse.RightButtonClick(); InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE); break;
                    case 2: InputSimulator.Keyboard.KeyPress(VirtualKeyCode.SHIFT); break;
                }

                await Task.Delay(random.Next(100, 200));

                if (CheckPosition()) return;

                InputSimulator.Mouse.RightButtonClick();
                InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                await Task.Delay(random.Next(100, 200));

                if (CheckPosition()) return;

            }

            if (random.Next(5) == 4)
            {
                InputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                for (int k = 0; k < random.Next(4) + 1; k++)
                {
                    InputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                }
                InputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            }

            InputSimulator.Mouse.VerticalScroll((random.Next(2) == 0 ? 1 : -1) * random.Next(7));
            await Task.Delay(random.Next(50, 80));
            InputSimulator.Mouse.LeftButtonClick();
            await Task.Delay(random.Next(50, 80));
            InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

            if (CheckPosition()) return;

            Console.Write("    ");
            Console.Write(DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}
