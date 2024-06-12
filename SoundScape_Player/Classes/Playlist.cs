using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Test1_Base
{
    public class Playlist
    {
        public Playlist() { }

        public Playlist(string name, string tracks, string image)
        {
            this.name = name;
            this.tracks = tracks;
			this.image = image;
        }

        public string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

        public string tracks;

		public string Tracks
		{
			get { return tracks; }
			set { tracks = value; }
		}

		private string image;

		public string Image
		{
			get { return image; }
			set { image = value; }
		}

		public int id { get; set; }

		public int userId;
        public int UserId 
		{
			get { return userId; }
			set { userId = value; } 
		}

		public virtual User User { get; set; }
    }
}
