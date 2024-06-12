using System;
using System.Collections.Generic;
using System.ComponentModel;
using NAudio.Wave;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1_Base
{
    public class CuttingContext : INotifyPropertyChanged
    {
        private double maximum;
        private double minimum;

        private double upperValue;
        private double lowerValue;

        private TimeSpan upperValueTS;
        private TimeSpan lowerValueTS;

        private string name;
        private string path;

        public CuttingContext()
        {
            Minimum = 0;
            Maximum = 1;
            UpperValue = Maximum;
            LowerValue = Minimum;
        }

        public double Maximum
        {
            get { return maximum; }
            
            set 
            {
                maximum = value;
                UpperValue = value;
                OnPropertyChanged("Maximum");
            }
        }

        public double Minimum
        { 
            get { return minimum; } 
            set {  minimum = value; } 
        }

        public double UpperValue
        {
            get { return upperValue; }
            set 
            { 
                upperValue = value;
                UpperValueTS = TimeSpan.FromTicks((long)(upperValue * TimeSpan.TicksPerSecond));
                OnPropertyChanged("UpperValue");
            }
        }

        public double LowerValue
        {
            get { return lowerValue; }
            set 
            { 
                lowerValue = value;
                LowerValueTS = TimeSpan.FromTicks((long)(lowerValue * TimeSpan.TicksPerSecond));
                OnPropertyChanged("LowerValue");
            }
        }

        public TimeSpan LowerValueTS
        { 
            get { return lowerValueTS; } 
            set 
            { 
                lowerValueTS = value;
                OnPropertyChanged("LowerValueTS");
            } 
        }

        public TimeSpan UpperValueTS
        {
            get { return upperValueTS; }
            set 
            { 
                upperValueTS = value;
                OnPropertyChanged("UpperValueTS");
            }
        }

        public string Path
        {
            set 
            {
                path = value;
                Name = System.IO.Path.GetFileName(value);
                var file = new MediaFoundationReader(path);
                Maximum = file.TotalTime.TotalSeconds;

                OnPropertyChanged("Path");
            }
            get { return path; }
        }

        public string Name
        {
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
            get { return name; }
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
