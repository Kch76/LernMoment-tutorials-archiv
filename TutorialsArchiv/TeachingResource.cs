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

        public TeachingResource() { }

        public TeachingResource(string theTitle, string theUrl, MediumType theMedium, TargetAudience theAudience)
        {
            Title = theTitle;
            Url = theUrl;
            Medium = theMedium;
            Audience = theAudience;
        }
    }
}
