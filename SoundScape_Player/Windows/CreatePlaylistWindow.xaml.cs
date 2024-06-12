using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для CreatePlaylistWindow.xaml
    /// </summary>
    public partial class CreatePlaylistWindow : Window
    {
        private string imagePath;
        public CreatePlaylistWindow()
        {
            InitializeComponent();

            
        }

        private void addCover_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                imagePath = ofd.FileName;
                Image image = addCover.FindName("cover") as Image;
                image.Source = new BitmapImage(new Uri(imagePath));
            }
            else
            {
                imagePath = null;
            }
        }

        private void createPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.DataStorage.MyPlaylists.Find(n => n.Name == tbTitle.Text) != null)
            {
                MessageBox.Show("Плейлист с таким названием уже существует");
                return;
            }
            MainWindow.DataStorage.MyPlaylists.Add(new MyPlaylist(tbTitle.Text, imagePath, null, MainWindow.DataStorage.User));

            UpdateContentRequested?.Invoke(this, EventArgs.Empty);

            this.Close();

        }

        public static EventHandler UpdateContentRequested;
    }
}
