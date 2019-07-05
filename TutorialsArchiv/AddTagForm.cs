using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialsArchiv
{
    public partial class AddTagForm : Form
    {
        public string NewTag { get; private set; }
        public bool WasCanceled { get; private set; }

        private bool _isTagValid = false;

        public AddTagForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            NewTag = tagTextBox.Text;
            WasCanceled = false;

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NewTag = string.Empty;
            WasCanceled = true;

            this.Close();
        }

        private void TagTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (tagTextBox.Text.Length > 0)
            {
                addButton.Enabled = true;
                cancelButton.Enabled = true;
            }
            else
            {
                addButton.Enabled = false;
                cancelButton.Enabled = false;
            }
        }
    }
}
