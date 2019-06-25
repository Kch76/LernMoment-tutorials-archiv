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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string entry = $"{titelTextBox.Text}, {urlTextBox.Text}";
            File.AppendAllText("file-database.csv", entry);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("file-database.csv"))
            {
                IEnumerable<string> entries = File.ReadLines("file-database.csv");
                string firstEntry = entries.First();
                string[] elementsOfFirstEntry = firstEntry.Split(',');

                titelTextBox.Text = elementsOfFirstEntry[0];
                urlTextBox.Text = elementsOfFirstEntry[1];
            }
        }
    }
}
