using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using ExtensionMethods;
using Test1_Base.Classes;


namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataStorage DataStorage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            playlistFrame.Navigate(new MainPage());
            DataStorage = new DataStorage();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            playlistFrame.Navigate(new MainPage());
        }

        private void profile_Click(object sender, RoutedEventArgs e)
        {
            AuthRegWindow authReg = new AuthRegWindow();
            authReg.Show();
        }

        private void MainProjectWindow_Closed(object sender, EventArgs e)
        {
            DataStorage.DataSave();

            Environment.Exit(0);
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var path = Path.Combine(exePath, "Help\\Help.pdf");
            Process.Start(path);
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

    }
}
