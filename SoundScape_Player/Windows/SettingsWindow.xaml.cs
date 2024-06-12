using System.Windows;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            this.DataContext = MainPage.MyPlayer;
            InitializeComponent();
        }

        private void resetBalance_Click(object sender, RoutedEventArgs e)
        {
            balance.Value = 0d;
        }

        private void resetPlaybackSpeed_Click(object sender, RoutedEventArgs e)
        {
            playbackSpeed.Value = 1d;
        }
    }
}
