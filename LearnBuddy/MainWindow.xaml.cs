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
        Login login;
        Registration registration;

        public MainWindow()
        {
            InitializeComponent();
            registration = new Registration(this);
            login = new Login(this);
        }

        private void btn_Registration_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            registration.ShowRegistration();
        }

        private void btn_Login_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            login.ShowLogin();
        }
    }
}