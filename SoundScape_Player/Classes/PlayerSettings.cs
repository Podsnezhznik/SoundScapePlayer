using System.Threading;

namespace Test1_Base
{
    public partial class Player 
    {
        private double playbackSpeed = 1;

        private double balance = 0;

        private double saveVolume = 0.5;

        public double PlaybackSpeed
        {
            get { return playbackSpeed; } 
            set 
            {  
                playbackSpeed = value;

                player.Pause();

                player.SpeedRatio = playbackSpeed;
                
                Thread.Sleep(500);
                player.Play();

                OnPropertyChanged("PlaybackSpeed");
            }
            
        }

        public double Balance
        {
            set
            {
                balance = value;
                player.Balance = balance;
                OnPropertyChanged("Balance");
            }
            get { return balance; }
        }

        public double SaveVolume
        {
            set
            {
                saveVolume = value;
                player.Volume = saveVolume;
                OnPropertyChanged("SaveVolume");
            }
            get { return saveVolume; }
        }
    }
}
