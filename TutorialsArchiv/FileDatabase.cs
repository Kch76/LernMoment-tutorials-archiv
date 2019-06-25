using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    class FileDatabase
    {
        private readonly string _fileName;

        public FileDatabase(string fileName)
        {
            _fileName = fileName;
        }

        public void Save(string entry)
        {
            File.AppendAllText(_fileName, entry);
        }

        public string LoadFirstEntry()
        {
            if (File.Exists(_fileName))
            {
                IEnumerable<string> entries = File.ReadLines(_fileName);
                return entries.First();
            }

            return string.Empty;
        }
    }
}
