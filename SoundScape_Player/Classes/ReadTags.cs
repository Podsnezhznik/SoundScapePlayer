using System;

namespace Test1_Base
{
    internal class ReadTags
    {
        private string path;
        private TagLib.File audioTag;
        public ReadTags(string path) 
        {
            if (path != null && path != string.Empty)
            {
                this.path = path;

                try
                {
                    audioTag = TagLib.File.Create(path);
                }
                catch (Exception)
                {
                    audioTag = null;
                    
                }
                
            }
        }

        public string ReadPerformers()
        {
            if(audioTag == null)
            {
                return string.Empty;
            }

            return String.Join(", ", audioTag.Tag.Performers);
        }

        public string ReadTitle()
        {
            if (audioTag == null)
            {
                return string.Empty;
            }

            return audioTag.Tag.Title;
        }

        public void SetPerformers(string performers)
        {
            audioTag.Tag.Performers = performers.Split(',', ';');
            audioTag.Save();
        }

        public void SetTitle(string title) 
        { 
            audioTag.Tag.Title = title;
            audioTag.Save();
        }
    }
}
