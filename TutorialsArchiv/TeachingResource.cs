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

    public enum TargetAudience
    {
        beginner,
        advanced,
        expert
    }

    public class TeachingResource
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public MediumType Medium { get; set; }
        public TargetAudience Audience { get; set; }

        public TeachingResource(string theTitle, string theUrl, MediumType theMedium, TargetAudience theAudience)
        {
            Title = theTitle;
            Url = theUrl;
            Medium = theMedium;
            Audience = theAudience;
        }

        public string ToCsvLine()
        {
            return $"{Title};{Url};{Medium.ToString()};{Audience.ToString()}";
        }

        public static TeachingResource BuildFromCsvLine(string csvLine)
        {
            string[] elements = csvLine.Split(';');
            Enum.TryParse(elements[2], out MediumType medium);
            Enum.TryParse(elements[3], out TargetAudience audience);
            return new TeachingResource(elements[0], elements[1], medium, audience);
        }
    }
}
