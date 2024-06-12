using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using NAudio.Wave;
using NAudio.Dmo.Effect;
using Microsoft.Win32;
using NAudio.Wave.SampleProviders;
using System.IO;
using NAudio.FileFormats.Mp3;
using TagLib;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        WaveOut player = new WaveOut();
        private TimeSpan position;
        private TimeSpan duration;
        Mp3FileReader reader;

        private DirectSoundOut output = null;

        private BlockAlignReductionStream stream = null;

        private string path = "G:\\учеба\\DiplomTest\\Test1_Base\\music\\1.mp3";

        public MainWindow()
        {

            InitializeComponent();

            reader = new Mp3FileReader("G:\\учеба\\DiplomTest\\Test1_Base\\music\\1.mp3");

            player.Init(reader);




        }

        public TimeSpan Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }  

        public TimeSpan Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }  
        
        

        private void qwe_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == false) return;

            WaveChannel32 wave = new WaveChannel32(new WaveFileReader(open.FileName));
            EffectStream effect = new EffectStream(wave);
            stream = new BlockAlignReductionStream(effect);

            output = new DirectSoundOut(200);
            output.Init(stream);
            output.Play();
        }

        private void asd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == false) return;

            var inputReader = new AudioFileReader(open.FileName);
            
            var stereo = new MonoToStereoSampleProvider(inputReader);
            stereo.LeftVolume = 0.0f; // silence in left channel
            stereo.RightVolume = 1.0f; // full volume in right channel

            player.Init(stereo);
            player.Play();

        }

        private void zxc_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == false) return;
            if (Path.GetExtension(open.FileName) == ".mp3") ConvertToWave(open.FileName);
            var inputReader = new AudioFileReader(open.FileName);
            
            var mono = new StereoToMonoSampleProvider(inputReader);
            mono.LeftVolume = 0.0f; // discard the left channel
            mono.RightVolume = 1.0f; // keep the right channel

            player.Init(mono);

            player.Play();
        }


        public string ConvertToWave(string fileName)
        {
            string outfile = fileName.Trim(new char[] { '.', 'm', 'p', '3'}) + ".wav";

            TagLib.File audioTagMp3, audioTagWav;
            audioTagMp3 = TagLib.WavPack.File.Create(fileName);
            string title = audioTagMp3.Tag.Title;
            string[] author = audioTagMp3.Tag.Performers;
            string album = audioTagMp3.Tag.Album;

            using (var reader = new Mp3FileReader(fileName))
            {
                WaveFileWriter.CreateWaveFile(outfile, reader);
            }

            audioTagWav = TagLib.File.Create(outfile);
            audioTagWav.Mode = TagLib.File.AccessMode.Write;

            audioTagWav.Tag.Album = "Kukla Kolduna".ToLower(); //audioTagMp3.Tag.Album.ToString();

            audioTagWav.Tag.Title = "Kukla Kolduna".ToLower(); //audioTagMp3.Tag.Title.ToString();

            audioTagWav.Tag.Performers = new string[] { "qwe" };
            audioTagWav.Save();
            //System.IO.File.Delete(fileName);
            return outfile;
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

    public class EffectStream : WaveStream
    {
        public WaveStream SourceStream { get; set; }

        public EffectStream(WaveStream stream)
        {
            SourceStream = stream;
        }

        public override WaveFormat WaveFormat => SourceStream.WaveFormat;

        public override long Length => SourceStream.Length;

        public override long Position 
        { 
            get => SourceStream.Position; 
            set { SourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return SourceStream.Read(buffer, offset, count);
        }
    }


}
