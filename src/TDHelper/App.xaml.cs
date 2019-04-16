﻿using System.Windows;
using Services;

namespace TDHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IdleWorker _idleWorker;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _idleWorker = new IdleWorker();
            
            var window = new MainWindow();
            window.Show();
        }
    }
}
