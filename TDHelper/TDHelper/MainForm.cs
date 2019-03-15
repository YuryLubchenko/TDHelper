using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace TDHelper
{
    public partial class MainForm : Form
    {
        private const string TimeSpanFormat = "hh\\:mm\\:ss";

        private Timer Timer { get; }

        private const int TimerInterval = 1000;

        private TimeSpan IdleThreshold => TimeSpan.FromSeconds(10);

        private DateTime Started { get; set; }

        private InputSimulator InputSimulator { get; }

        public MainForm()
        {
            InitializeComponent();

            Timer = new Timer();

            InputSimulator = new InputSimulator();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Started = DateTime.Now;

            Timer.Interval = TimerInterval;
            Timer.Enabled = true;

            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var idleTime = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());

            idleTextBox.Text = idleTime.ToString(TimeSpanFormat);
            startedOnTextBox.Text = (DateTime.Now - Started).ToString(TimeSpanFormat);

            if (idleTime < IdleThreshold)
                return;

            InputSimulator.Keyboard.KeyPress(VirtualKeyCode.CONTROL);
        }
    }
}
