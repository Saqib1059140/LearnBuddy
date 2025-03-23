using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LearnBuddy
{
    internal class ShowDashboard
    {
        Dbase db = new Dbase();
        private DispatcherTimer refreshTimer;
        private Dashboard _dashBoard;

        public ShowDashboard(Dashboard dashboard)
        {
            _dashBoard = dashboard;
        }

        public void Show()
        {
            string query = 
                $"SELECT f.Bezeichnung Fach, ng.Beschreibung Beschreibung, ng.Status Status " +
                $"FROM nachhilfegesuch ng " +
                $"join fach f on ng.fachid = f.fachid " +
                $"where ng.status = 'offen'";

            _dashBoard.dg_Dashboard_Public.ItemsSource = db.QueryToDataTable(query).DefaultView;
        }

        public void Refresh()
        {
            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromSeconds(3);
            refreshTimer.Tick += (sender, e) => Show();
            refreshTimer.Start();
        }
    }
}