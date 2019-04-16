using System;
using System.Windows;
using System.Windows.Threading;
using Services;

namespace TDHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TimeSpanFormat = "hh\\:mm\\:ss";

        private DispatcherTimer Timer { get; }

        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(1);

        private DateTime Started { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Started = DateTime.Now;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = TimerInterval;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var idleTime = TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime());

            IdleTextBox.Text = idleTime.ToString(TimeSpanFormat);
            StartedOnTextBox.Text = (DateTime.Now - Started).ToString(TimeSpanFormat);
        }
    }
}
