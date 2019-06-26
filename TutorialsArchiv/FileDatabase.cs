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

        public IEnumerable<TeachingResource> LoadAllEntries()
        {
            List<TeachingResource> allResources = new List<TeachingResource>();

            if (File.Exists(_fileName))
            {
                IEnumerable<string> csvLines = File.ReadLines(_fileName);
                foreach (string entry in csvLines)
                {
                    allResources.Add(TeachingResource.BuildFromCsvLine(entry));
                }
            }

            return allResources;
        }
    }
}
