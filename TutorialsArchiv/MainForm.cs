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
        private FileDatabase _db = null;

        public MainForm()
        {
            InitializeComponent();
            _db = new FileDatabase("file-database.csv");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string entry = $"{titelTextBox.Text}, {urlTextBox.Text}";
            _db.Save(entry);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string firstEntry = _db.LoadFirstEntry();
            if (firstEntry != string.Empty)
            {
                string[] elementsOfFirstEntry = firstEntry.Split(',');

                titelTextBox.Text = elementsOfFirstEntry[0];
                urlTextBox.Text = elementsOfFirstEntry[1];
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;
        }
    }
}
