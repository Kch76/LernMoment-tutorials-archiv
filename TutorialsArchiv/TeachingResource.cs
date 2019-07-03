using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    public class TeachingResource
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Medium { get; set; }

        public TeachingResource(string theTitle, string theUrl, string theMedium)
        {
            Title = theTitle;
            Url = theUrl;
            Medium = theMedium;
        }

        public string ToCsvLine()
        {
            return $"{Title};{Url};{Medium}";
        }

        public static TeachingResource BuildFromCsvLine(string csvLine)
        {
            string[] elements = csvLine.Split(';');
            return new TeachingResource(elements[0], elements[1], elements[2]);
        }
    }
}
