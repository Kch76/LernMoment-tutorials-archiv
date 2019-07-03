using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    public enum MediumType
    {
        Buch,
        Video,
        Artikel,
        Kurs,
        Podcast
    }

    public class TeachingResource
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public MediumType Medium { get; set; }

        public TeachingResource(string theTitle, string theUrl, MediumType theMedium)
        {
            Title = theTitle;
            Url = theUrl;
            Medium = theMedium;
        }

        public string ToCsvLine()
        {
            return $"{Title};{Url};{Medium.ToString()}";
        }

        public static TeachingResource BuildFromCsvLine(string csvLine)
        {
            string[] elements = csvLine.Split(';');
            Enum.TryParse(elements[2], out MediumType medium);
            return new TeachingResource(elements[0], elements[1], medium);
        }
    }
}
