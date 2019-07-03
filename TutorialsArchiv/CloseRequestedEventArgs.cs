using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    public class CloseRequestedEventArgs : EventArgs
    {
        public CloseRequestedEventArgs()
        {
            ForceClose = true;
        }

        public bool ForceClose { get; set; }
    }
}
