using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace Test1_Base.Classes
{
    public class DataStorage : INotifyPropertyChanged
    {
        public DataStorage()
        {
            myPlaylists = new List<MyPlaylist>();

            user = new User();
        }

        private List<MyPlaylist> myPlaylists;
        public List<MyPlaylist> MyPlaylists
        {
            get => myPlaylists;
            set
            {
                myPlaylists = value;
                OnPropertyChanged("MyPlaylists");
            }
        }

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        private bool isLogged;

        public bool IsLogged
        {
            get { return isLogged; }
            set { isLogged = value; }
        }

        public void LogOut()
        {
            MyPlaylists = new List<MyPlaylist>();

            User = new User();

            IsLogged = false;
        }

        public void DataSave()
        {
            MainPage.MyPlayer.Pause();
            using (AppContext appContext = new AppContext())
            {
                var playlists = appContext.Playlists.Where(c => c.UserId == User.userId).ToList() ?? new List<Playlist>();

                foreach (var playlist in playlists)
                {
                    playlist.Tracks = MyPlaylists.Single(c => c.Name == playlist.Name).ConvertToPlaylist().Tracks;

                    playlist.Image = MyPlaylists.Single(c => c.Name == playlist.Name).ConvertToPlaylist().Image;
                }

                foreach (var myPlaylist in MyPlaylists)
                {
                    if (!playlists.Contains(myPlaylist))
                    {
                        appContext.Playlists.Add(myPlaylist.ConvertToPlaylist());
                    }
                }

                appContext.SaveChanges();
            }

        }

        public void Registrate(string password, string login)
        {
            User user = new User(login, password);

            Playlist playlist = new Playlist("Мои аудиозаписи", string.Empty, string.Empty) { User = user };

            using (AppContext db = new AppContext())
            {
                try
                {
                    db.Users.Add(user);
                    db.Playlists.Add(playlist);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация успешна");
                }
                catch (Exception)
                {
                    MessageBox.Show("Данный логин занят");
                }
            }
        }

        public void Authorize(string login, string password)
        {
            List<User> users = new List<User>();
            using (AppContext context = new AppContext())
            {
                users = context.Users.ToList();

                foreach (User user in users)
                {
                    if (login == user.Login && MyHash.CompareLogin(password, user.Password))
                    {
                        MainWindow.DataStorage.IsLogged = true;
                        MainWindow.DataStorage.User = user;

                        List<Playlist> intermediatePlaylist;

                        var play = from p in context.Playlists
                                   where p.UserId == user.userId
                                   select p;
                        intermediatePlaylist = play.ToList();

                        foreach (Playlist playlist in intermediatePlaylist)
                        {
                            MyPlaylists.Add(new MyPlaylist(playlist));
                        }
                    }
                }

                if (!IsLogged)
                {
                    MessageBox.Show("Пользователь не найден");
                }

                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
