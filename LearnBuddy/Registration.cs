using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace LearnBuddy
{
    class Registration
    {
        Dbase db;
        Universal universal;
        private MainWindow _mainwindow;

        public Registration(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
            db = new Dbase();
            universal = new Universal(mainWindow);
        }

        public void ShowRegistration()
        {
            _mainwindow.Registrate.Visibility = System.Windows.Visibility.Visible;
            _mainwindow.Registrate.IsSelected = true;
        }

        public void ShowSelectionChangedRegistration()
        {
            if (_mainwindow.cb_Selection_Registrate.SelectedIndex == 0)
            {
                _mainwindow.Grid_Tutoring_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.StackPanel_Tutor_Registrate.Visibility = System.Windows.Visibility.Collapsed;

                FillCbData();
            }
            else if (_mainwindow.cb_Selection_Registrate.SelectedIndex == 1)
            {
                _mainwindow.Grid_Tutoring_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.StackPanel_Tutor_Registrate.Visibility = System.Windows.Visibility.Visible;

                FillCbData();
            }
        }

        public void FillCbData()
        {
            GetCbDataClass();
            GetCbDataCourse();
            GetCbDataSubject();
        }

        public void GetCbDataClass()
        {
            string query = $"SELECT Bezeichnung FROM klasse";

            _mainwindow.cb_Class_Registrate.Items.Clear();
            try
            {
                DataTable dt = db.QueryToDataTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    _mainwindow.cb_Class_Registrate.Items.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Daten: " + ex.Message);
            }
        }

        public void GetCbDataCourse()
        {
            string query = $"SELECT Bezeichnung FROM bildungsgang";

            _mainwindow.cb_Course_Registrate.Items.Clear();
            try
            {
                DataTable dt = db.QueryToDataTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    _mainwindow.cb_Course_Registrate.Items.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Daten: " + ex.Message);
            }
        }

        public void GetCbDataSubject()
        {
            string query = $"SELECT Bezeichnung FROM fach";

            _mainwindow.cb_Subject_Registrate.Items.Clear();
            try
            {
                DataTable dt = db.QueryToDataTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    _mainwindow.cb_Subject_Registrate.Items.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Daten: " + ex.Message);
            }
        }

        public void AddTutoring()
        {
            string name = _mainwindow.tb_Name_Registrate.Text.Trim();
            string lastname = _mainwindow.tb_LastName_Registrate.Text.Trim();
            string gender = _mainwindow.cb_Gender_Registrate.Text.Trim();
            string email = _mainwindow.tb_Email_Registrate.Text.Trim();
            string subject = _mainwindow.cb_Subject_Registrate.Text.Trim();
            string remarks = _mainwindow.tb_Remarks_Registrate.Text.Trim();
            string CreatetAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string course = _mainwindow.cb_Course_Registrate.Text.Trim();

            try
            {
                db.AuthenticateStudentAndGetData(name, lastname, gender, email, subject, course);
                if (db.studentExists == true)
                {
                    db.ExecuteQuery(
                        $"INSERT INTO nachhilfegesuch (`SchuelerID`, `FachID`, `BildungsgangsID`, `Beschreibung`, `ErstelltAm`, `Status`) " +
                        $"VALUES ('{db.StudentID}','{db.FachID}','{db.BildungsgangsID}','{remarks}','{CreatetAt}','Offen')");

                    MessageBox.Show("Nachhilfe gesuch wurde Gespeichert");
                    universal.Logout();
                }
                else
                {
                    MessageBox.Show("Schüler existiert nicht");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Hinzufügen der Nachhilfegesuch: " + ex.Message);
            }
        }
            
        public void AddTutor()
        {
            string email = _mainwindow.tb_Username_Registrate.Text.Trim();
            string password = _mainwindow.tb_Password_Registrate.Text.Trim();

            try
            {
                if (db.AuthenticateTuterAndGetData(email))
                {
                    // Create Login
                    // db.ExecuteQuery($"INSERT INTO login(`E-Mail`, Passwort, admin) VALUES ('{email}', '{password}', '0')");

                    // Create Tutor
                    db.ExecuteQuery($"INSERT INTO `tutor`(`SchuelerID`, `Genehmigt`, `LoginID`) VALUES ('{db.StudentID}','0','7')");
                    MessageBox.Show($"Die Nachfrage von für Tutor wurde dem Admin weitergeleitet !");
                    universal.Logout();
                }
                else
                {
                    MessageBox.Show("Tutor existiert nicht");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Hinzufügen tutor: " + ex.Message);
            }
        }
    }
}
