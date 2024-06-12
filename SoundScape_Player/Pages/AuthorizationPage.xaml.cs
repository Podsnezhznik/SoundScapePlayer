using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Navigation;
using System.Threading.Tasks;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void regPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/RegistrationPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();

            MainWindow.DataStorage.Authorize(login, password);

            if (MainWindow.DataStorage.IsLogged) 
                NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/ProfilePage.xaml", UriKind.RelativeOrAbsolute));

        }

    }
}
