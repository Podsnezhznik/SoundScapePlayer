using System.Collections.Generic;
using System.IO;

namespace Test1_Base
{
    public class MixingFilesContext
    {
        public MixingFilesContext() 
        {
            listOfMixedFiles = new List<string>();

            namesOfMixedFiles = new List<Names>();
        }

        private List<string> listOfMixedFiles;

        private List<Names> namesOfMixedFiles;

        public List<string> ListOfMixedFiles
        {
            get { return listOfMixedFiles; }
            set 
            { 
                listOfMixedFiles = value;
            }
        }
        public List<Names> NamesOfMixedFiles
        {
            get 
            {
                return namesOfMixedFiles; 
            }
            set 
            {
                namesOfMixedFiles = value;
            }
        }

        public void AddFiles(string[] files)
        {
            foreach (string file in files) 
            {
                ListOfMixedFiles.Add(file);

                NamesOfMixedFiles.Add(new Names(Path.GetFileName(file)));
            }
        }
    }

    public class Names
    {
        public Names(string name)
        {
            Name = name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
