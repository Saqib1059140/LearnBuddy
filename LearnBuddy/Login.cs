using Database;
using LearnBuddy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnBuddy
{
    class Login
    {
        Dbase db;
        Universal universal;
        Tutor tutor;
        Admin admin;
        private MainWindow _mainwindow;

        public Login(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
            db = new Dbase();
            universal = new Universal(mainWindow);
            tutor = new Tutor(mainWindow);
            admin = new Admin(mainWindow);
        }

        public void ShowTabitemLogin()
        {
            _mainwindow.Login.Visibility = System.Windows.Visibility.Visible;
            _mainwindow.Login.IsSelected = true;
        }

        public void CkeckUser()
        {
            string email = _mainwindow.tb_Username_Login.Text.Trim();
            string password = _mainwindow.pb_Passwort_Login.Password.Trim();

            try
            {
                db.ConfirmLogin(email, password);

                if (db.correctLogin)
                {
                    if (db.isAdmin == true)
                    {
                        admin.ShowAdminDashboard();
                    }
                    else
                    {
                        tutor.Fill_cb_FilterSubject_Tutor();
                        tutor.Fill_dg_ShowTutoring_Tutor();
                        tutor.ShowTutorDashboard();
                    }
                }
                else
                {
                    MessageBox.Show("Email oder Passwort ist Falsch, Bitte überprüfen");
                }
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}