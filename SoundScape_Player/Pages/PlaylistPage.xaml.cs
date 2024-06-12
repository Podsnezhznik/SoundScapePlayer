using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : Page
    {
        public PlaylistPage()
        {
            this.DataContext = MainWindow.DataStorage.MyPlaylists;

            InitializeComponent();

            CreatePlaylistWindow.UpdateContentRequested += UpdateContentRequest;
        }

        private void createPlaylist_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylistWindow createPlaylistWindow = new CreatePlaylistWindow();
            createPlaylistWindow.ShowDialog();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var playlist = list.SelectedItem as MyPlaylist;

            PlaylistViewPage.MyCurrentPlaylist = playlist;

            NavigationService.GetNavigationService(this).Navigate(new Uri("Pages/PlaylistViewPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void UpdateContentRequest(object sender, EventArgs e) 
        {
            list.SelectionChanged -= list_SelectionChanged;

            list.ItemsSource = null;

            list.ItemsSource = MainWindow.DataStorage.MyPlaylists;

            list.SelectionChanged += list_SelectionChanged;
        }
    }
}
