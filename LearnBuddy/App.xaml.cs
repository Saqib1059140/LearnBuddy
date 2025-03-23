﻿using System.Configuration;
using System.Data;
using System.Windows;

namespace LearnBuddy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
