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
        Tutor tutor;
        Universal universal;

        public MainWindow()
        {
            InitializeComponent();
            registration = new Registration(this);
            login = new Login(this);
            tutor = new Tutor(this);
            universal = new Universal(this);
        }

        // Button Configs

        private void btn_Registration_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            registration.ShowRegistration();
        }

        private void btn_Login_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            login.ShowTabitemLogin();
        }

        private void btn_Logout_Tutor_Click(object sender, RoutedEventArgs e)
        {
            universal.Logout();
        }

        private void btn_Login_Login_Click(object sender, RoutedEventArgs e)
        {
            login.CkeckUser();
        }

        private void btn_Logout_Login_Click(object sender, RoutedEventArgs e)
        {
            universal.Logout();
        }

        private void btn_Back_Registrate_Click(object sender, RoutedEventArgs e)
        {
            universal.Logout();
        }

        private void btn_ConfirmTutoringRegistration_Registrate_Click(object sender, RoutedEventArgs e)
        {
            registration.AddTutoring();
            universal.Logout();

        }

        private void btn_ConfirmTutorRegistration_Registrate_Click(object sender, RoutedEventArgs e)
        {
            registration.AddTutor();
            universal.Logout();
        }

        // ComboBox Configs

        private void cb_FilterSubject_Tutor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tutor.Filtered_dgShowTutoring_Tutor();
        }

        private void cb_Selection_Registrate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            registration.ShowSelectionChangedRegistration();
        }
    }
}