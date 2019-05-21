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

        private static readonly TimeSpan IdleThreshold = TimeSpan.FromSeconds(10);

        private readonly Random _random = new Random();

        private readonly InputSimulator _inputSimulator = new InputSimulator();

        private readonly List<Action> _actions;

        public IdleWorker()
        {
            LastMousePosition = IdleTimeFinder.GetMousePosition();

            _actions = GetActions();

            _timer = new Timer(Callback, null, TimeSpan.FromMilliseconds(0), TimerInterval);
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        private List<Action> GetActions()
        {
            var keyboardActions = new List<Action>
            {
                () => _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LCONTROL),
                () => _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RCONTROL),
                () => _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.LSHIFT),
                () => _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RSHIFT),
                () => _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE),
                () =>
                {
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    for(int i = 1, n = _random.Next(1, 6); i <= n; i++)
                    {
                        Thread.Sleep(100);
                        _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.TAB);
                    }
                    _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                }

            };

            var mouseActions = new List<Action>
            {
                () => _inputSimulator.Mouse.LeftButtonClick(),
                () =>
                {
                    _inputSimulator.Mouse.RightButtonClick();
                    Thread.Sleep(50);
                    _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                },
                () => _inputSimulator.Mouse.VerticalScroll(_random.Next(1, 5)),
                () => _inputSimulator.Mouse.VerticalScroll(-1 * _random.Next(1, 5))
            };

            var actions = new List<Action>();

            for (int i = 0; i < 1; i++)
            {
                actions.AddRange(keyboardActions);
            }

            for (int i = 0; i < 5; i++)
            {
                actions.AddRange(mouseActions);
            }

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

            for (int i = 0; i < _random.Next(40, 80); i++)
            {
                if (MouseMoved())
                    return;

                _actions[_random.Next(_actions.Count)]();

                await Task.Delay(50);
                
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);

                await Task.Delay(_random.Next(100, 200));
            }
        }
    }
}
