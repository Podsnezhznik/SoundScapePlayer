using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для ExtractingPage.xaml
    /// </summary>
    public partial class ExtractingPage : Page
    {
        public ExtractingPage()
        {
            InitializeComponent();
        }

        private async void extractFromVideo_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => NAudioUsing.ExtractFromVideo());
        }
    }
}
