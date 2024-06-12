using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.DataContext = MainWindow.DataStorage.User;

            InitializeComponent();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.DataStorage.DataSave();

            MainWindow.DataStorage.LogOut();

            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/RegistrationPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
