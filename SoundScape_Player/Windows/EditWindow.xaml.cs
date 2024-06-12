using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using NAudio.Wave;
using System.IO;
using System.Threading;
using Test1_Base.Pages;
//using System.Web.UI.WebControls;

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        System.Windows.Controls.Button prevButton;

        public EditWindow()
        {
            InitializeComponent();

            mixingFiles.Click += PaintButton;
            concatenatingFiles.Click += PaintButton;
            convertChannels.Click += PaintButton;
            convertFiles.Click += PaintButton;
            cuttingFiles.Click += PaintButton;
            extractingFiles.Click += PaintButton;
        }

        private void convertFiles_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new ConvertingPage();
        }

        private void convertChannels_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new ChannelPage();
        }

        private void mixingFiles_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new MixingPage();
        }

        private void concatenatingFiles_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new ConcatenatingPage();
        }

        private void cuttingFiles_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new CuttingPage();
        }

        private void extractingFiles_Click(object sender, RoutedEventArgs e)
        {
            editFrame.Content = new ExtractingPage();
        }

        private void PaintButton(object sender, RoutedEventArgs e)
        {
            if(prevButton != null)
            {
                prevButton.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));

                prevButton.BorderBrush = new SolidColorBrush(Color.FromRgb(103, 58, 183));

                prevButton.Foreground = Brushes.White;
            }

            System.Windows.Controls.Button currentButton = (System.Windows.Controls.Button)sender;

            currentButton.Background = Brushes.Transparent;

            currentButton.BorderBrush = new SolidColorBrush(Color.FromRgb(103, 58, 183));

            currentButton.Foreground = Brushes.Black;

            prevButton = currentButton;
        }

        
    }
}
