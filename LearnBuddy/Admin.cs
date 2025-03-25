using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBuddy
{
    class Admin
    {
        private MainWindow _mainwindow;

        public Admin(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
        }

        public void ShowAdminDashboard()
        {
            _mainwindow.Admin.IsSelected = true;
            _mainwindow.Login.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
