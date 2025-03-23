using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearnBuddy
{
    public partial class Dashboard : Window
    {
        ShowDashboard showDashboard;

        public Dashboard()
        {
            InitializeComponent();
            showDashboard = new ShowDashboard(this);
            showDashboard.Show();
            showDashboard.Refresh();
        }
    }
}
