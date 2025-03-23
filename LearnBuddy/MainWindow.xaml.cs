using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnBuddy
{
    public partial class MainWindow : Window
    {
        //Login login = new Login();
        Dashboard dashboard = new Dashboard();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Registration_Dashboard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}