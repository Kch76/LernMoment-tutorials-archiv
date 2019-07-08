using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    public class ExportDataEventArgs : EventArgs
    {
        public ExportDataEventArgs(string newFileNameAndPath)
        {
            this.NewFileNameAndPath = newFileNameAndPath;
        }

        public string NewFileNameAndPath { get; private set; }
    }
}
