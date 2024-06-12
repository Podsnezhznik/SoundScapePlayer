using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test1_Base.Windows
{
    /// <summary>
    /// Логика взаимодействия для TrackEditWindow.xaml
    /// </summary>
    public partial class TrackEditWindow : Window
    {
        private Track track = new Track();
        public Track Track
        {
            get => track;
            set => track = value;
        }

        public TrackEditWindow(Track track)
        {
            this.DataContext = track;

            InitializeComponent();

            this.track = track;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
