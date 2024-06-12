using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1_Base
{
    public class TrackList
    {
        List<Track> tracks;
        public TrackList(string tracksPath) 
        {
            tracks = new List<Track>();

            foreach(var trackPath in tracksPath.Split(';').ToList())
            {
                if (trackPath != string.Empty)
                {
                    this.tracks.Add(new Track(trackPath));
                }
                
            }

            indexating();
        }

        public TrackList()
        {
            tracks = new List<Track>();
        }

        public List<Track> Tracks
        { 
            get { return tracks; } 
        }

        public void RemoveTrack(int index)
        {
            tracks.RemoveAt(index);

            indexating();
        }
        
        public void RemoveTrack(Track track)
        {
            tracks.RemoveAt(track.Index);

            indexating();
        }

         

        public void AddTrack(string track, int index = -1)
        {
            Track newTrack = new Track(track);

            if(index >= 0)
            {
                newTrack.Index = index;
                tracks.Add(newTrack);
            }
            else
            {
                tracks.Add(newTrack);
                indexating();
            }

            
        }

        public void indexating()
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                tracks[i].Index = i;
            }
        }

        public string ConvertToString()
        {
            string str = string.Empty;
            foreach (var track in tracks)
            { 
                str += track.Path + ";";
            }

            return str;
        }
    }
}
