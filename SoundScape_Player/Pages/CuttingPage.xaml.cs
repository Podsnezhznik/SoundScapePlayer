using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для CuttingPage.xaml
    /// </summary>
    public partial class CuttingPage : Page
    {
        private CuttingContext cuttingContext;
        public CuttingContext CuttingContext
        {
            get { return cuttingContext; } 
            set { cuttingContext = value; } 
        }
            

        public CuttingPage()
        {
            CuttingContext = new CuttingContext();

            this.DataContext = CuttingContext;

            InitializeComponent();
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Audio Files|*.mp3";

            if (ofd.ShowDialog() == false) return;

            CuttingContext.Path = ofd.FileName;
        }

        private async void saveFile_Click(object sender, RoutedEventArgs e)
        {
            if (Path.GetExtension(cuttingContext.Path) == ".mp3") 
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = ".mp3 Files|*.mp3";

                if(sfd.ShowDialog() == false) return;

                await Task.Run(() => NAudioUsing.TrimMp3File(cuttingContext.Path, sfd.FileName, (int)CuttingContext.LowerValue, (int)CuttingContext.UpperValue));
            }
            else
            {
                MessageBox.Show("Необходимо выбрать .mp3 файл");
            }
        }
    }
}
