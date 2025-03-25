using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBuddy
{
    class Universal
    {
        private MainWindow _mainwindow;

        public Universal(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
        }

        public void Logout()
        {
            _mainwindow.Dashboard.IsSelected = true;
            _mainwindow.Dashboard_Tutor.Visibility = System.Windows.Visibility.Collapsed;
            _mainwindow.Registrate.Visibility = System.Windows.Visibility.Collapsed;
            _mainwindow.Login.Visibility = System.Windows.Visibility.Collapsed;
            _mainwindow.Admin.Visibility = System.Windows.Visibility.Collapsed;
            _mainwindow.tb_Username_Login.Clear();
            _mainwindow.pb_Passwort_Login.Clear();
        }

        public void ShowTutorDashboard()
        {
            _mainwindow.Dashboard_Tutor.IsSelected = true;
            _mainwindow.Login.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void ShowAdminDashboard()
        {
            _mainwindow.Admin.IsSelected = true; 
            _mainwindow.Login.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
