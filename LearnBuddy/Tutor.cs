using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace LearnBuddy
{
    class Tutor
    {
        Dbase db = new Dbase();
        private MainWindow _mainwindow;

        public Tutor(MainWindow mainWindow)
        {
            _mainwindow = mainWindow;
        }

        public void Fill_cb_FilterSubject_Tutor()
        {
            string query = $"SELECT bezeichnung FROM fach";
            try
            {
                DataTable dt = db.QueryToDataTable(query);
                foreach (DataRow row in dt.Rows)
                {
                    _mainwindow.cb_FilterSubject_Tutor.Items.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Filtered_dgShowTutoring_Tutor()
        {
                string query =
                $"SELECT GROUP_CONCAT(CONCAT(s.Vorname, ' ', s.Nachname) SEPARATOR ', ') AS Schüler, f.Bezeichnung AS Fach, " +
                $"ng.Beschreibung, ng.Status " +
                $"FROM nachhilfegesuch ng " +
                $"JOIN Fach f ON ng.FachID = f.FachID " +
                $"JOIN Schueler s ON ng.SchuelerID = s.SchuelerID " +
                $"WHERE f.Bezeichnung = '{_mainwindow.cb_FilterSubject_Tutor.SelectedItem.ToString()}' " +
                $"and ng.status = 'offen' " +
                $"GROUP BY f.Bezeichnung, ng.Beschreibung, ng.Status";

            try
            {
                _mainwindow.dg_ShowTutoring_Tutor.ItemsSource = db.QueryToDataTable(query).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Übersichtsdaten: " + ex.Message);
            }
        }

        public void Fill_dg_ShowTutoring_Tutor()
        {
            string query =
                    $"SELECT GROUP_CONCAT(CONCAT(s.Vorname, ' ', s.Nachname) SEPARATOR ', ') AS Schüler, f.Bezeichnung AS Fach, " +
                    $"ng.Beschreibung, ng.Status " +
                    $"FROM nachhilfegesuch ng " +
                    $"JOIN Fach f ON ng.FachID = f.FachID " +
                    $"JOIN Schueler s ON ng.SchuelerID = s.SchuelerID " +
                    $"where ng.status = 'offen' " +
                    $"GROUP BY f.Bezeichnung, ng.Beschreibung, ng.Status ";

            try
            {
                _mainwindow.dg_ShowTutoring_Tutor.ItemsSource = db.QueryToDataTable(query).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Übersichtsdaten: " + ex.Message);
            }
        }
    }
}
