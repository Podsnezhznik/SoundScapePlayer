using Microsoft.Win32;
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
    /// Логика взаимодействия для MixingPage.xaml
    /// </summary>
    public partial class MixingPage : Page
    {
        private MixingFilesContext context;
        public MixingFilesContext Context 
        {
            get => context;
            set => context = value; 
        }

        public MixingPage()
        {
            Context = new MixingFilesContext();

            this.DataContext = Context;

            InitializeComponent();
        }

        private void addMixedFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Аудио файлы|*.mp3;*.wav;*.mva;*.aac";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == false) return;

            Context.AddFiles(openFileDialog.FileNames);

            RefreshListView();
        }

        private async void startMixing_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".wav File|*.wav";

            if (saveFileDialog.ShowDialog() == false) return;

            await Task.Run(() => NAudioUsing.MixFiles(saveFileDialog.FileName, Context.ListOfMixedFiles));
        }

        public void RefreshListView()
        {
            listOfNames.ItemsSource = null;

            listOfNames.ItemsSource = Context.NamesOfMixedFiles;
        }
    }
}
