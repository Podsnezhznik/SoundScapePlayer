using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagLib;
using TagLib.Matroska;
using Test1_Base.Windows;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для PlaylistViewPage.xaml
    /// </summary>
    public partial class PlaylistViewPage : Page
    {
        private static MyPlaylist myCurrentPlaylist = new MyPlaylist();
        public static MyPlaylist MyCurrentPlaylist
        {
            get {  return myCurrentPlaylist; }
            set { myCurrentPlaylist = value; }
        }

        public PlaylistViewPage()
        {
            this.DataContext = MainPage.MyPlayer;

            InitializeComponent();

            list.ItemsSource = MyCurrentPlaylist.TrackList.Tracks;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/PlaylistPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Track track = list.SelectedItem as Track;

            if(track != null && !MainPage.MyPlayer.IsPlayingPath(track.Path))
            {
                MainPage.MyPlayer.MainPlaylist = MyCurrentPlaylist.TrackList;

                MainPage.MyPlayer.IndexOfTrack = track.Index;

                MainPage.MyPlayer.IsPlaylistPlaying = true;
            }
        }

        private void addTracksInPlaylist_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            open.FilterIndex = 2;
            open.Multiselect = true;

            if (open.ShowDialog() == true)
            {
                foreach (var item in open.FileNames)
                {
                    MyCurrentPlaylist.TrackList.AddTrack(item);
                }
            }

            RefreshPage();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            MyPlaylist item = MainWindow.DataStorage.MyPlaylists.FirstOrDefault(p => p.Name == MyCurrentPlaylist.Name) as MyPlaylist;

            item.TrackList = MyCurrentPlaylist.TrackList;

            item.Image = MyCurrentPlaylist.Image;
        }

        private void deleteTrack_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;

            Track track = button.DataContext as Track;

            MyCurrentPlaylist.TrackList.RemoveTrack(track.Index);

            RefreshPage();
        }

        private void RefreshPage()
        {
            list.ItemsSource = null;

            list.ItemsSource = MyCurrentPlaylist.TrackList.Tracks;
        }

        private void playlistPlay_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MyPlayer.MainPlaylist = MyCurrentPlaylist.TrackList;

            MainPage.MyPlayer.IndexOfTrack = 0;

            MainPage.MyPlayer.IsPlaylistPlaying = true;

            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void editTrack_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;

            Track track = button.DataContext as Track;

            TrackEditWindow window = new TrackEditWindow(track);

            window.Show();
        }

        private void changeCover_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico;*.tif;*.tiff";

            if (dialog.ShowDialog() == false) return;

            MyCurrentPlaylist.Image = new BitmapImage(new Uri(dialog.FileName));

            cover.Source = MyCurrentPlaylist.Image;
        }
    }
}
