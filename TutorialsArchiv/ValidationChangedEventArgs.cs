using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    public class ValidationChangedEventArgs : EventArgs
    {
        public ValidationChangedEventArgs(string propertyName, bool isValid)
        {
            PropertyName = propertyName;
            IsValid = isValid;
        }

        public string PropertyName { get; private set; }
        public bool IsValid { get; private set; }
    }
}
