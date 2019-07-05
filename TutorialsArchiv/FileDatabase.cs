using CsvHelper;
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
            using (var writer = new StreamWriter(_fileName))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = ";";

                csv.WriteRecords(entries);
                writer.Flush();
            }
        }

        public IEnumerable<TeachingResource> LoadAllEntries()
        {
            List<TeachingResource> allResources = new List<TeachingResource>();

            using (var reader = new StreamReader(_fileName))
            using (var csv = new CsvReader(reader))
            {
                allResources = csv.GetRecords<TeachingResource>().ToList();
            }

            return allResources;
        }
    }
}
