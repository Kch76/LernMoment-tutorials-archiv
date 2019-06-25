using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialsArchiv
{
    public partial class MainForm : Form
    {
        private readonly FileDatabase _db = null;

        public MainForm()
        {
            InitializeComponent();
            _db = new FileDatabase("file-database.csv");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _db.Save(new TeachingResource(titelTextBox.Text, urlTextBox.Text));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TeachingResource firstEntry = _db.LoadFirstEntry();
            if (firstEntry != null)
            {
                titelTextBox.Text = firstEntry.Title;
                urlTextBox.Text = firstEntry.Url;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;
        }
    }
}
