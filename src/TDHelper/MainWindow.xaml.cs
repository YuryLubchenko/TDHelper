using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Services;

namespace TDHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string TimeSpanFormat = @"dd\.hh\:mm\:ss";

        private DispatcherTimer Timer { get; }

        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(1);

        private DateTime Started { get; }

        private NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            CreateNotificationIcon();

            Started = DateTime.Now;

            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
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
            _notifyIcon = new NotifyIcon {Icon = new System.Drawing.Icon("icon.ico"), Visible = true};
            _notifyIcon.DoubleClick += NotifyIconOnDoubleClick;
        }

        private void NotifyIconOnDoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            ShowInTaskbar = true;
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

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _notifyIcon.DoubleClick -= NotifyIconOnDoubleClick;
            _notifyIcon?.Dispose();
        }
    }
}