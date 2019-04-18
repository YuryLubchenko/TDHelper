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

            CreateNotificationIcon();

            Started = DateTime.Now;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = TimerInterval;
            Timer.Start();

            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var idleTime = IdleTimeFinder.GetIdleTime();

            IdleTextBox.Text = idleTime.ToString(TimeSpanFormat);
            StartedOnTextBox.Text = (DateTime.Now - Started).ToString(TimeSpanFormat);
        }

        private void CreateNotificationIcon()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("icon.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Show();
                    WindowState = WindowState.Normal;
                    ShowInTaskbar = true;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                ShowInTaskbar = false;
            }

            base.OnStateChanged(e);
        }
    }
}
