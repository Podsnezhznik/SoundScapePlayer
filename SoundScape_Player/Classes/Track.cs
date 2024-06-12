using System.Windows;

namespace Test1_Base
{
    public class Track
    {
        private string title;
        private string author;
        private string path;
        private int index;
        ReadTags readTags;

        public Track()
        {
            
        }

        public Track(string path)
        {
            readTags = new ReadTags(path);
            this.path = path;
            author = readTags.ReadPerformers();
            title = readTags.ReadTitle();
        }

        public string Title 
        {
            get { return title; }
            set 
            { 
                
                if (MainPage.MyPlayer.IsPlayingPath(Path))
                {
                    MessageBox.Show("Перед изменение метаданных файла остановите воспроизведение и закройте файл");
                    return;
                }
                title = value;
                readTags.SetTitle(title);
            } 
        }
        public string Author
        {
            get { return author; }
            set 
            { 
                
                if (MainPage.MyPlayer.IsPlayingPath(Path))
                {
                    MessageBox.Show("Перед изменение метаданных файла остановите воспроизведение и закройте файл");
                    return;
                }
                author = value;
                readTags.SetPerformers(author);
            } 
        }
        public string Path
        {
            get { return path; }
            set { path = value; } 
        }
        public int Index 
        {
            get { return index; }
            set {  index = value; }
        }
    }
}
