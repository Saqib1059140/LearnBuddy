﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBuddy
{
    class Login
    {
        private MainWindow _mainwindow;

        public Login(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
        }

        public void ShowLogin()
        {
            _mainwindow.Login.Visibility = System.Windows.Visibility.Visible;
            _mainwindow.Login.IsSelected = true;
        }
    }
}
