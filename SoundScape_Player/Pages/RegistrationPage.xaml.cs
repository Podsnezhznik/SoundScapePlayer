using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ExtensionMethods;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void authPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/AuthorizationPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            if (login.Rules().MinCharacters(5).Validate() &&
                password.Rules().MinCharacters(5).Validate() &&
                passwordConfirm.Rules().MinCharacters(5).EqualTo(password).Validate())
            {
                MainWindow.DataStorage.Registrate(MyHash.GenerateMD5Hash(password.Password.Trim()), login.Text.Trim());

                NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/AuthorizationPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
