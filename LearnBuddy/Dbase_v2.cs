using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database
{
    /// <summary>
    /// class Dbase
    /// creates a connection object to a database and contains 
    /// methods to manage database operations
    /// </summary>
    class Dbase
    {
        private string serverName = "localhost";
        private string databaseName = "learnBuddy";
        private string uid = "root";
        private string passwd = "";
        private string connString = string.Empty;
        private MySqlConnection? connection = null;
        private MySqlCommand? command = null;
        public bool correctLogin = false;
        public bool studentExists = false;
        public int StudentID = 0;
        public int FachID = 0;


        #region Konstruktor
        /// <summary>
        /// Verbindung mit der Datenbank mit Standardwerten, außer des Datenbanknamens.
        /// </summary>
        /// <param name="_database">Name der Datenbank</param>

        public Dbase()
        {
            Connect();
        }
        #endregion

        /// <summary>
        /// Stellt eine neue Verbindung mit der Datenbank her und testet diese.
        /// </summary>
        private void Connect()
        {
            connString = $"SERVER={serverName};DATABASE={databaseName};UID={uid};PWD={passwd}";
            try
            {
                connection = new MySqlConnection(connString);
                connection.Open();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
                connection = null;
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Trennt eine Verbindung zur Datenbank.
        /// </summary>
        private void Disconnect()
        {
            command?.Dispose();
            command = null;
            connection?.Dispose();
            connection = null;
        }

        /// <summary>
        /// Gibt true zurück, wenn eine Verbindung zur Datenbank besteht.
        /// </summary>
        public bool ConnectionExist
        {
            get
            {
                if (connection == null) return false;
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Führt allgemeine Querys, wie "INSERT INTO", "UPDATE", "DELETE FROM" etc. aus.
        /// </summary>
        /// <param name="_query">Auszuführende SQL-Query</param>
        public void ExecuteQuery(string _query)
        {
            try
            {
                connection.Open();
                command = new MySqlCommand(_query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
                throw new Exception(ex.Message);
            }
        }

        #region ToDataTable
        /// <summary>
        /// Führt eine SQL-Query aus und gibt diese in Form eines DataTables zurück.
        /// </summary>
        /// <param name="_query">Auszuführende SQL-Query</param>
        /// <returns>Rückgabe des befüllten DataTables</returns>
        public DataTable QueryToDataTable(string _query)
        {
            DataTable dtData = new DataTable();
            try
            {
                connection.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter(_query, connection);
                adp.Fill(dtData);
                connection.Close();
                return dtData;
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
                throw new Exception(ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// Führt eine SQL-Query aus, die nur eine Tabelle besitzt, und gibt diese als DataTable zurück. 
        /// </summary>
        /// <param name="_table">Name der auszugebenden Tabelle</param>
        /// <returns>Rückgabe des befüllten DataTables</returns>
        public DataTable TableToDataTable(string _table)
        {
            DataTable dtData = new DataTable();
            string query = $"SELECT * FROM {_table}";
            try
            {
                connection.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter(query, connection);
                adp.Fill(dtData);
                connection.Close();
                return dtData;
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
                throw new Exception(ex.Message);
                return new DataTable();
            }
        }
        #endregion

        public bool QueryToBool(string query)
        {
            try
            {
                connection.Open();
                command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                bool hasRows = reader.HasRows;
                reader.Close();
                connection.Close();
                return hasRows;
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
                throw new Exception(ex.Message);
                return false;
            }
        }

        public bool ConfirmLogin(string username, string password)
        {
            string query = $"SELECT Count(*) FROM `login` WHERE `E-Mail` = @username AND Passwort = SHA2(@password, 256)";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    correctLogin = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
                throw new Exception("Datenbankfehler: " + ex.Message);
            }

            return correctLogin;
        }

        public bool AuthenticateStudent(string name, string lastname)
        {
            int studentId = -1;

            string query =
                $"SELECT s.vorname, s.nachname " +
                $"FROM schueler s " +
                $"WHERE s.vorname LIKE @name " +
                $"AND s.nachname = @lastname ";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name + "%");
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        studentExists = reader.HasRows;
                    }
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Datenbankfehler: " + ex.Message);
            }

            if (!studentExists)
            {
                throw new Exception("Fehler: Der Schüler existiert nicht in der Datenbank.");
            }

            string getStudentID = $"select schuelerID from schueler s where s.vorname = '{name}' and s.nachname = '{lastname}'";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(getStudentID, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentId = reader.GetInt32(0); // Indexbasiertes Abrufen der ID
                            StudentID = studentId;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: {ex}");
            }

            return studentExists;

        }

        public bool AddStudentTutoring(string name, string lastname, string fach)
        {
            int studentId = -1;

            // Überprüft ob der schüler in der DB existiert
            string query =
                $"SELECT s.vorname, s.nachname FROM schueler s WHERE s.vorname LIKE @name AND s.nachname = @lastname ";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name + "%");
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        studentExists = reader.HasRows;
                    }
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Datenbankfehler: " + ex.Message);
            }

            if (!studentExists)
            {
                throw new Exception("Fehler: Der Schüler existiert nicht in der Datenbank.");
            }

            // holt die id vom Schüler
            string queryGetStudentID = $"select schuelerID from schueler s where s.vorname = @name and s.nachname = @lastname";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(queryGetStudentID, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentId = reader.GetInt32(0); // Indexbasiertes Abrufen der ID
                            StudentID = studentId;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: {ex}");
            }

            // holt die Fachid
            string queryGetStudentFachID = $"select fachid from fach f where f.Bezeichnung = @fach";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(queryGetStudentFachID, connection))
                {
                    cmd.Parameters.AddWithValue("@fach", fach);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FachID = reader.GetInt32(0);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler: {ex}");
            }

            return studentExists;
        }

        public int AuthenticateExistingStudentAndGetId(string name, string lastname, string gender, string email, string Class, string course, string subject)
        {
            int studentId = -1;
            int subjectId = -1;

            // Überprüft ob der schüler in der DB existiert
            string CheckStudentExists =
                $"SELECT s.vorname, s.nachname " +
                $"FROM schueler s " +
                $"JOIN bildungsgang b ON s.bildungsgangid = b.bildungsgangid " +
                $"WHERE s.vorname LIKE @name " +
                $"AND s.nachname = @lastname ";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(CheckStudentExists, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name + "%");
                    cmd.Parameters.AddWithValue("@lastname", lastname);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentId = reader.GetInt32("schuelerid");
                        }
                    }
                    connection.Close();
                }

                if (studentId == -1)
                {
                    throw new Exception("Fehler: Der Schüler existiert nicht in der Datenbank.");
                }

                // holt die id vom Schüler

                string subjectQuery = @" SELECT fachid FROM fach WHERE bezeichnung = @subject";

                string getStudentID = $"select schuelerID from schueler s where s.vorname = '{name}' and s.nachname = '{lastname}'";

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(getStudentID, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@lastname", lastname);

                        connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentId = reader.GetInt32(0); // Indexbasiertes Abrufen der ID
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fehler: {ex}");
                }

                if (studentId == -1)
                {
                    throw new Exception("Fehler: Der Schüler existiert nicht in der Datenbank.");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Datenbankfehler: " + ex.Message);
            }

            return studentId;
        }



        public DataTable ExecuteParameterizedQuery(string query, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    adp.Fill(dt);
                }
                connection.Close();
                return dt;
            }
            catch (MySqlException ex)
            {
                if (connection?.State == ConnectionState.Open)
                    connection.Close();
                throw new Exception("Fehler bei ExecuteParameterizedQuery: " + ex.Message);
            }
        }
    }
}