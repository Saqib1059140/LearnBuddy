using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBuddy
{
    class Registration
    {
        private MainWindow _mainwindow;

        public Registration(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
        }

        public void ShowRegistration()
        {
            _mainwindow.Registrate.Visibility = System.Windows.Visibility.Visible;
            _mainwindow.Registrate.IsSelected = true;
        }
    }
}
