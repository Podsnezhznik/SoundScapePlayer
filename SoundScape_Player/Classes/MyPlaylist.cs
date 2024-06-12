using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.IO;

namespace Test1_Base
{
    public class MyPlaylist
    {
        string name;
        BitmapImage image;
        TrackList tracks;

        public MyPlaylist()
        {

        }

        public MyPlaylist(string name, string image, TrackList tracks, User user)
        {
            this.name = name;

            if (image == string.Empty || image == null) 
            {
                this.image = null;
            }
            else 
            {
                this .image = new BitmapImage(new Uri(image));
            }
                
            this.tracks = tracks ?? new TrackList();
            userId = user.userId;
        }

        public MyPlaylist(Playlist playlist)
        {
            name = playlist.Name;

            tracks = new TrackList(playlist.tracks);

            List<int> indecex = new List<int>();
            for(int i = 0; i < tracks.Tracks.Count; i++)
            {
                if (!File.Exists(tracks.Tracks[i].Path))
                {
                    tracks.RemoveTrack(tracks.Tracks[i]);
                    i--;
                }
            }
            

            if (playlist.Image == string.Empty || playlist.Image == null)
            {
                image = null;
            }
            else
            {
                image = new BitmapImage(new Uri(playlist.Image));
            }

            userId = playlist.UserId;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public TrackList TrackList
        {
            get { return tracks; }
            set { tracks = value; }
        }
        public BitmapImage Image
        {
            get { return image; }
            set { image = value; }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }


        public Playlist ConvertToPlaylist ()
        {
            string tracks = this.TrackList.ConvertToString();
            var playlist = new Playlist(Name, tracks, image?.UriSource.ToString() ?? string.Empty);
            playlist.UserId = userId;

            return playlist;
        }
    }
}
