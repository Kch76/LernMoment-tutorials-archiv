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

        public void Save(TeachingResource entry)
        {
            File.AppendAllText(_fileName, entry.ToCsvLine());
        }

        public TeachingResource LoadFirstEntry()
        {
            if (File.Exists(_fileName))
            {
                IEnumerable<string> entries = File.ReadLines(_fileName);
                return TeachingResource.BuildFromCsvLine(entries.First());
            }

            return null;
        }
    }
}
