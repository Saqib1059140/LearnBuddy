using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnBuddy
{
    class Registration
    {
        Dbase db;
        private MainWindow _mainwindow;

        public Registration(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
            db = new Dbase();
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
                // make Tutor Registration invisible
                _mainwindow.lbl_Password_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.tb_Password_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.tb_Password_Registrate.Clear();

                _mainwindow.btn_ConfirmTutorRegistration_Registrate.Visibility = System.Windows.Visibility.Collapsed;

                // make Tutoring Registration visible
                _mainwindow.lbl_Appointment_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.cb_Appointment_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.cb_Appointment_Registrate.Text = "Termin Wählen";

                _mainwindow.lbl_Remarks_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.tb_Remarks_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.tb_Remarks_Registrate.Clear();

                _mainwindow.btn_ConfirmTutoringRegistration_Registrate.Visibility = System.Windows.Visibility.Visible;

                FillCbData();
            }
            else if (_mainwindow.cb_Selection_Registrate.SelectedIndex == 1)
            {
                // make Tutoring Registration invisible
                _mainwindow.lbl_Appointment_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.cb_Appointment_Registrate.Visibility = System.Windows.Visibility.Collapsed;

                _mainwindow.lbl_Remarks_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.tb_Remarks_Registrate.Visibility = System.Windows.Visibility.Collapsed;
                _mainwindow.tb_Remarks_Registrate.Clear();

                _mainwindow.btn_ConfirmTutoringRegistration_Registrate.Visibility = System.Windows.Visibility.Collapsed;

                // make Tutor Registration visible
                _mainwindow.lbl_Password_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.tb_Password_Registrate.Visibility = System.Windows.Visibility.Visible;
                _mainwindow.tb_Password_Registrate.Clear();

                _mainwindow.btn_ConfirmTutorRegistration_Registrate.Visibility = System.Windows.Visibility.Visible;

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
    }
}
