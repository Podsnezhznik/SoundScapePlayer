using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Test1_Base
{
    public class NAudioUsing
    {
        public static string CreateOpenFile(string filter = "")
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;

            if (ofd.ShowDialog() != true) return string.Empty;

            return ofd.FileName;
        }

        public static void ConvertToMp3(Track trackWav = null)
        {
            if(trackWav  == null)
                trackWav = new Track(CreateOpenFile("wav files (*.wav)|*.wav"));

            string outfile = trackWav.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + ".mp3";

            using (var reader = new MediaFoundationReader(trackWav.Path))
            {
                MediaFoundationEncoder.EncodeToMp3(reader, outfile);
            }

            Track trackMp3 = new Track(outfile);

            trackMp3.Title = trackWav.Title;
            trackMp3.Author = trackWav.Author;
        }

        public static void ConvertToWav(Track trackMp3 = null)
        {
            if (trackMp3 == null)
                trackMp3 = new Track(CreateOpenFile("mp3 files (*.mp3)|*.mp3"));

            string outfile = trackMp3.Path.Trim(new char[] { '.', 'm', 'p', '3' }) + ".wav";

            using (var reader = new Mp3FileReader(trackMp3.Path))
            {
                WaveFileWriter.CreateWaveFile(outfile, reader);
            }

            Track trackWav = new Track(outfile);

            trackWav.Title = trackMp3.Title;
            trackWav.Author = trackMp3.Author;
        }
        
        public static void ConvertToWma(Track trackWav = null)
        {
            if (trackWav == null)
                trackWav = new Track(CreateOpenFile("wav files (*.wav)|*.wav"));

            string outfile = trackWav.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + ".wmv";

            using (var reader = new WaveFileReader(trackWav.Path))
            {
                MediaFoundationEncoder.EncodeToWma(reader, outfile);
            }

            Track trackWmv = new Track(outfile);

            trackWmv.Title = trackWav.Title;
            trackWmv.Author = trackWav.Author;
        }

        public static void ConvertToAac(Track trackWav = null)
        {
            if (trackWav == null)
                trackWav = new Track(CreateOpenFile("wav files (*.wav)|*.wav"));

            string outfile = trackWav.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + ".aac";

            using (var reader = new WaveFileReader(trackWav.Path))
            {
                MediaFoundationEncoder.EncodeToAac(reader, outfile);
            }

            Track trackAac = new Track(outfile);

            trackAac.Title = trackWav.Title;
            trackAac.Author = trackWav.Author;
        }

        public static void ExtractFromVideo()
        {
            string videoPath = CreateOpenFile("Video Files|*.asf;*.mp4;*.mkv;*.flv");

            string outfile = videoPath.Trim(Path.GetExtension(videoPath).ToCharArray()) + ".mp3";
            
            using (var reader = new MediaFoundationReader(videoPath))
            {
                MediaFoundationEncoder.EncodeToMp3(reader, outfile);
            }
        }

        public static void ConvertToStereo(Track monoTrack = null)
        {
            if (monoTrack == null) 
                monoTrack = new Track(CreateOpenFile());

            if (monoTrack.Path == string.Empty) return;

            try
            {
                AudioFileReader inputReader = new AudioFileReader(monoTrack.Path);
                MonoToStereoSampleProvider stereo = new MonoToStereoSampleProvider(inputReader);
                stereo.LeftVolume = 1f;
                stereo.RightVolume = 1f;

                string outfile; // = stereoTrack.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + "_Mono_.wav";

                switch (Path.GetExtension(monoTrack.Path))
                {
                    case ".mp3":
                        outfile = monoTrack.Path.Trim(new char[] { '.', 'm', 'p', '3' }) + "_Stereo_";
                        WaveFileWriter.CreateWaveFile16(outfile, stereo);
                        ConvertToMp3(new Track(outfile));
                        File.Delete(outfile);
                        break;

                    default:
                        outfile = monoTrack.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + "_Stereo_.wav";
                        WaveFileWriter.CreateWaveFile16(outfile, stereo);
                        Track stereoTrack = new Track(outfile);
                        monoTrack.Author = stereoTrack.Author;
                        monoTrack.Title = stereoTrack.Title;
                        break;
                } 
            }
            catch(System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ConvertToMono(Track stereoTrack = null)
        {
            if (stereoTrack == null)
                stereoTrack = new Track(CreateOpenFile());

            if (stereoTrack.Path == string.Empty) return;

            try
            {
                AudioFileReader inputReader = new AudioFileReader(stereoTrack.Path);
                StereoToMonoSampleProvider mono = new StereoToMonoSampleProvider(inputReader);
                mono.LeftVolume = 0.0f;
                mono.RightVolume = 1.0f;

                string outfile; // = stereoTrack.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + "_Mono_.wav";

                switch (Path.GetExtension(stereoTrack.Path))
                {
                    case ".mp3":
                        outfile = stereoTrack.Path.Trim(new char[] { '.', 'm', 'p', '3' }) + "_Mono_";
                        WaveFileWriter.CreateWaveFile16(outfile, mono);
                        ConvertToMp3(new Track(outfile));
                        File.Delete(outfile);
                        break;

                    default:
                        outfile = stereoTrack.Path.Trim(new char[] { '.', 'w', 'a', 'v' }) + "_Mono_.wav";
                        WaveFileWriter.CreateWaveFile16(outfile, mono);
                        Track monoTrack = new Track(outfile);
                        monoTrack.Author = stereoTrack.Author;
                        monoTrack.Title = stereoTrack.Title;
                        break;
                }
            }
            catch(System.ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static void MixFiles(string outFile, List<string> path)
        {
            var list = new List<AudioFileReader>();

            foreach (var item in path) 
            {
                var reader = new AudioFileReader(item);
                reader.Volume = 0.75f;
                list.Add(reader);
            }

            var mixer = new MixingSampleProvider(list);

            WaveFileWriter.CreateWaveFile16(outFile, mixer);
        }

        public static void ConcatenateFiles(string outFile, List<string> path)
        {
            var list = new List<AudioFileReader>();

            foreach (var item in path)
            {
                var reader = new AudioFileReader(item);
                reader.Volume = 0.75f;
                list.Add(reader);
            }

            var playlist = new ConcatenatingSampleProvider(list);

            WaveFileWriter.CreateWaveFile16(outFile, playlist);
        }


        public static void TrimMp3File(string inputFilePath, string outputFolderPath, int beginIntSec, int endIntSec)
        {
            using (var reader = new Mp3FileReader(inputFilePath))
            {

                int bytesPerSecond = reader.WaveFormat.AverageBytesPerSecond;
                int bytesPerSegment = bytesPerSecond * (endIntSec - beginIntSec);

                reader.Position = beginIntSec * bytesPerSecond;

                byte[] buffer = new byte[bytesPerSegment];
                int bytesRead;

                bytesRead = reader.Read(buffer, 0, buffer.Length);
                {
                    using (var writer = new WaveFileWriter(outputFolderPath, reader.WaveFormat))
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

    }
}
