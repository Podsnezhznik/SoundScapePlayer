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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test1_Base.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChannelPage.xaml
    /// </summary>
    public partial class ChannelPage : Page
    {
        public ChannelPage()
        {
            InitializeComponent();
        }

        private async void toStereo_Click(object sender, RoutedEventArgs e)
        {
           await Task.Run(() => NAudioUsing.ConvertToStereo(new Track(NAudioUsing.CreateOpenFile(string.Empty))));
        }

        private async void toMono_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => NAudioUsing.ConvertToMono(new Track(NAudioUsing.CreateOpenFile(string.Empty))));
        }
    }
}
