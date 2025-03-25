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
        Admin admin;
        Tutor tutor;
        private MainWindow _mainwindow;

        public Universal(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
            admin = new Admin(mainWindow);
            tutor = new Tutor(mainWindow);
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
            _mainwindow.tb_Name_Registrate.Clear();
            _mainwindow.tb_LastName_Registrate.Clear();
            _mainwindow.tb_Email_Registrate.Clear();
            _mainwindow.tb_Remarks_Registrate.Clear();
            _mainwindow.tb_Username_Registrate.Clear();
            _mainwindow.tb_Password_Registrate.Clear();

            _mainwindow.cb_Gender_Registrate.SelectedItem = null;
            _mainwindow.cb_Class_Registrate.SelectedItem = null;
            _mainwindow.cb_Course_Registrate.SelectedItem = null;
            _mainwindow.cb_Appointment_Registrate.SelectedItem = null;
            _mainwindow.cb_Selection_Registrate.SelectedItem = null;

            _mainwindow.StackPanel_Tutor_Registrate.Visibility = System.Windows.Visibility.Visible;
            _mainwindow.StackPanel_Tutor_Registrate.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}