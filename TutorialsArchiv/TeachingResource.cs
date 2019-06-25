using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    class TeachingResource
    {
        public string Title { get; private set; }
        public string Url { get; private set; }

        public TeachingResource(string theTitle, string theUrl)
        {
            Title = theTitle;
            Url = theUrl;
        }

        public string ToCsvLine()
        {
            return $"{Title},{Url}{Environment.NewLine}";
        }

        public static TeachingResource BuildFromCsvLine(string csvLine)
        {
            string[] elements = csvLine.Split(',');
            return new TeachingResource(elements[0], elements[1]);
        }
    }
}
