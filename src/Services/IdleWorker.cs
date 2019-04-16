using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Services
{
    public class IdleWorker
    {
        private (int X, int Y) LastMousePosition { get; set; }

        private readonly Timer _timer;

        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(1);

        private static readonly TimeSpan IdleThreshold = TimeSpan.FromSeconds(55);

        private readonly Random Random = new Random();

        private readonly InputSimulator InputSimulator = new InputSimulator();

        private readonly List<Action> Actions;

        public IdleWorker()
        {
            LastMousePosition = IdleTimeFinder.GetMousePosition();

            Actions = GetActions();

            _timer = new Timer(Callback, null, TimeSpan.FromMilliseconds(0), TimerInterval);
        }

        private List<Action> GetActions()
        {
            var actions = new List<Action>
            {
                () => InputSimulator.Keyboard.KeyPress(VirtualKeyCode.LCONTROL),
                () => InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RCONTROL),
                () => InputSimulator.Keyboard.KeyPress(VirtualKeyCode.LSHIFT),
                () => InputSimulator.Keyboard.KeyPress(VirtualKeyCode.RSHIFT),
                () => InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE),
                () => InputSimulator.Mouse.LeftButtonClick(),
                () =>
                {
                    InputSimulator.Mouse.RightButtonClick();
                    InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                },
                () => InputSimulator.Mouse.VerticalScroll((Random.Next(2) == 0 ? -1 : 1) * Random.Next(1, 5)),
                () =>
                {
                    InputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    for(int i = 1; i <= Random.Next(1, 6); i++)
                    {
                        InputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    }
                    InputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                }

            };

            return actions;
        }

        private bool MouseMoved()
        {
            var mPos = IdleTimeFinder.GetMousePosition();
            var lastPos = LastMousePosition;
            LastMousePosition = mPos;

            return (mPos.X != lastPos.X || mPos.Y != lastPos.Y);
        }

        private async void Callback(object state)
        {
            if (MouseMoved()) return;

            if (IdleTimeFinder.GetIdleTime() < IdleThreshold)
                return;

            for (int i = 0; i < Random.Next(20, 60); i++)
            {
                if (MouseMoved())
                    return;

                var action = Actions[Random.Next(Actions.Count)];

                InputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

                action();

                await Task.Delay(Random.Next(100, 200));                
            }
        }
    }
}
