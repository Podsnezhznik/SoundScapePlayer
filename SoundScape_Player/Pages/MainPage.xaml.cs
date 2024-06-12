using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private static Player myPlayer = new Player();

        public static Player MyPlayer
        {
            get { return myPlayer; }
            set { myPlayer = value; }
        }


        public MainPage()
        {
            InitializeComponent();

            this.DataContext = MyPlayer;
        }

        private void play_Click(object sender, RoutedEventArgs e) => MyPlayer.Play();

        private void pause_Click(object sender, RoutedEventArgs e) => MyPlayer.Pause();

        private void mute_Click(object sender, RoutedEventArgs e) => MyPlayer.Mute();

        private void unMute_Click(object sender, RoutedEventArgs e) => MyPlayer.UnMute();

        private void playlist_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.DataStorage.IsLogged)
            {
                NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/PlaylistPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show("Необходимо авторизоваться");
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow();
            edit.Show();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            open.FilterIndex = 2;
            open.RestoreDirectory = true;

            if (open.ShowDialog() == true)
            {
                MyPlayer.Path = open.FileName;

                MyPlayer.IsPlaylistPlaying = false;
            }
        }

        private void addTrack_Checked(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.DataStorage.IsLogged || MyPlayer.Path == string.Empty)
            {
                MessageBox.Show("Необходимо авторизироваться и выбрать аудиофайл");
                addTrack.IsChecked = false;
            }
            else
            {

                ListView listView = popupAddTrack.FindName("listOfPlaylists") as ListView;

                listView.ItemsSource = null;
                listView.ItemsSource = MainWindow.DataStorage.MyPlaylists;
                
            }
        }

        private void addInPlaylist_Click(object sender, RoutedEventArgs e)
        {
            MyPlaylist data = (MyPlaylist)((ToggleButton)sender).DataContext;
            foreach (var myPlaylist in MainWindow.DataStorage.MyPlaylists)
            {
                if(myPlaylist.Name == data.Name)
                {
                    myPlaylist.TrackList.AddTrack(MyPlayer.Path);
                }
            }
        }

        private void nextTrack_Click(object sender, RoutedEventArgs e)
        {
            MyPlayer.PlayNextTrack();
        }

        private void prevTrack_Click(object sender, RoutedEventArgs e)
        {
            MyPlayer.PlayPrevTrack();
        }

        private void clearPlayer_Click(object sender, RoutedEventArgs e)
        {
            MyPlayer.ClearPlayer();
        }
    }
}
