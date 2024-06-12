using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Test1_Base
{
    public partial class Player : INotifyPropertyChanged
    {
        private MediaPlayer player;

        private string path;

        private TimeSpan durationTS;

        private TimeSpan position;

        private ReadTags tags;

        private string author;

        private string title;

        private DispatcherTimer timer;

        private double progress;

        private bool isPlaying;

        private bool isMuted;

        private TrackList mainPlaylist;

        private int indexOfTrack;

        private bool isRepeat;

        private bool isShuffled;

        private TrackList shuffledPlaylist;

        private TrackList currentPlaylist;

        private bool isPlaylistPlaying;

        public Player()
        {
            player = new MediaPlayer();

            player.MediaOpened += Player_MediaOpened;
            player.MediaEnded += Player_MediaEnded;

            path = string.Empty;

            position = new TimeSpan(0, 0, 0);

            durationTS = new TimeSpan(0, 0, 0);

            tags = new ReadTags(path);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += Timer_Tick;

            isPlaying = false;
            
            isMuted = false;

            isRepeat = false;

            isPlaylistPlaying = false;

            shuffledPlaylist = new TrackList();
            currentPlaylist = new TrackList();
            mainPlaylist = new TrackList();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (position < durationTS)
            {
                Position = Position.Add(TimeSpan.Zero);
            }
            else
            {
                timer.Stop();
            }
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            DurationTS = player.NaturalDuration.TimeSpan;

        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            if (IsRepeat) 
            {
                Path = Path;
                Play();
            }
            else
            {
                if(IsPlaylistPlaying) 
                {
                    PlayNextTrack();
                }
            }
        }

        public string Path
        {
            set 
            { 
                if(!File.Exists(value) && value != string.Empty && value != null)
                {
                    MessageBox.Show("Не найден выбранный файл");
                    return;
                }

                path = value;
                player.Open(new Uri(path, UriKind.Relative));
                tags = new ReadTags(path);
                Author = tags.ReadPerformers();
                Title = tags.ReadTitle();
                Position = player.Position;

                if (IsPlaying)
                    Play();

                OnPropertyChanged("Path");
            }
            get { return path; }
        }

        public TimeSpan DurationTS
        {
            set 
            { 
                durationTS = value;
                OnPropertyChanged("DurationTS");
            }
            get { return durationTS; }
        }

        public string Author
        {
            set 
            { 
                author = value;
                OnPropertyChanged("Author");
            }
            get { return author; }
        }

        public string Title
        {
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
            get { return title; } 
        }

        public TimeSpan Position
        {
            set 
            {
                position = player.Position;
                Progress = position.TotalSeconds;
                OnPropertyChanged("Position");
            }
            get 
            { 
                return position; 
            }
        }

        public double Progress 
        { 
            set 
            {
                if (Math.Abs(value - player.Position.TotalSeconds) > 1.5)
                {
                    player.Position = TimeSpan.FromTicks((long)value * TimeSpan.TicksPerSecond); ;
                }
                progress = value;
                OnPropertyChanged("Progress");
            }
            get { return progress; }
        }

        public bool IsPlaying
        {
            set 
            { 
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
            get { return isPlaying; }
        }

        public bool IsMuted
        {
            set
            {
                isMuted = value;
                OnPropertyChanged("IsMuted");
            }
            get => isMuted;
        }

        public TrackList MainPlaylist
        { 
            get { return mainPlaylist; }
            set 
            {
                mainPlaylist = value;
                CurrentPlaylist = mainPlaylist;
                OnPropertyChanged("MainPlaylist");
            }
        }
        
        public TrackList CurrentPlaylist
        { 
            get { return currentPlaylist; }
            set 
            {
                currentPlaylist = value;
                OnPropertyChanged("CurrentPlaylist");
            }
        }

        public TrackList ShuffledPlaylist
        {
            get { return shuffledPlaylist; }
            set
            {
                shuffledPlaylist = value;
                
            }
        }

        public bool IsPlaylistPlaying 
        {
            get => isPlaylistPlaying;
            set
            {
                isPlaylistPlaying = value;
                OnPropertyChanged("IsPlaylistPlaying");
            }
        }

        public bool IsRepeat 
        {
            get { return isRepeat; }
            set { isRepeat  = value; }
        }
        
        public bool IsShuffled
        {
            get { return isShuffled; }
            set 
            {
                if (!IsPlaylistPlaying)
                {
                    return;
                }

                isShuffled = value;

                if (isShuffled)
                {
                    shuffledPlaylist = new TrackList();

                    Random random = new Random();
                    List<int> shuffledIndeсes = RandomSequence(random, MainPlaylist.Tracks.Count).Distinct().Take(MainPlaylist.Tracks.Count).ToList();

                    for (int i = 0; i < MainPlaylist.Tracks.Count; i++)
                    {
                        shuffledPlaylist.AddTrack(MainPlaylist.Tracks.FirstOrDefault(t => t.Index == shuffledIndeсes[i]).Path);
                    }

                    CurrentPlaylist = shuffledPlaylist;
                }
                else 
                {
                    CurrentPlaylist = MainPlaylist;
                }

                IndexOfTrack = CurrentPlaylist.Tracks.Count - 1;
                IndexOfTrack = 0;

            }
        }

        public int IndexOfTrack
        {
            get => indexOfTrack; 
            set 
            {
                if (value > CurrentPlaylist.Tracks.Count - 1)
                {
                    //return;
                    indexOfTrack = 0;
                    Path = CurrentPlaylist.Tracks.FirstOrDefault(t => t.Index == indexOfTrack).Path;
                    
                }
                else
                {
                    if (value < 0)
                    {
                        return;
                        indexOfTrack = 0;
                        Path = CurrentPlaylist.Tracks.FirstOrDefault(t => t.Index == indexOfTrack).Path;
                        
                    }
                    else
                    {
                        indexOfTrack = value;
                        Path = CurrentPlaylist.Tracks.FirstOrDefault(t => t.Index == indexOfTrack).Path;
                    }
                }

                Thread.Sleep(100);
                Play();

                OnPropertyChanged("IndexOfTrack");
            }
        }

        public void Play()
        {
            if (Path != string.Empty)
            {
                IsPlaying = true;
            }

            player.Play();

            timer.Start();
            
        }

        public void Pause() 
        {
            player.Pause();
            timer.Stop();
            IsPlaying= false;
        }

        public void Mute()
        {
            player.Volume = 0;
            IsMuted = true;
        }

        public void UnMute()
        {
            player.Volume = saveVolume;
            IsMuted = false;
        }

        public bool IsPlayingPath(string path)
        {
            return IsPlaying && Path == path;
        }

        public void PlayNextTrack()
        {
            if (!IsPlaylistPlaying) 
            {
                return;
            }

            IndexOfTrack += 1;

            Play();

        }

        public void PlayPrevTrack()
        {
            if (!IsPlaylistPlaying)
            {
                return;
            }

            IndexOfTrack -= 1;

            Play();

        }

        public void ClearPlayer()
        {
            player.Stop();

            player.Close();

            Path = string.Empty;

            DurationTS = TimeSpan.Zero;
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

        IEnumerable<int> RandomSequence(Random random, int max)
        {
            while (true)
            {
                yield return random.Next(max);
            }
        }
    }
}
