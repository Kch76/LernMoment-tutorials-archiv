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

        public void Save(IEnumerable<TeachingResource> entries)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }

            List<string> csvLines = ConvertToCsvLines(entries);

            File.AppendAllLines(_fileName, csvLines);
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

        private static List<string> ConvertToCsvLines(IEnumerable<TeachingResource> entries)
        {
            List<string> csvLines = new List<string>();
            foreach (var item in entries)
            {
                csvLines.Add(item.ToCsvLine());
            }

            return csvLines;
        }

    }
}
