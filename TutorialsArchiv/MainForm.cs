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
        private readonly List<TeachingResource> _allResources = null;

        public MainForm()
        {
            InitializeComponent();
            _allResources = new List<TeachingResource>();
            _db = new FileDatabase("file-database.csv");
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            _allResources.Add(new TeachingResource(titelTextBox.Text, urlTextBox.Text));
            _db.Save(_allResources);
            RefreshDGV();
            ClearEntryUIElements();
        }

        private void ClearEntryUIElements()
        {
            createButton.Enabled = false;
            createButton.Text = "Erstellt";

            cancelButton.Enabled = false;
            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _allResources.AddRange(_db.LoadAllEntries());
            RefreshDGV();
            ClearEntryUIElements();
        }

        private void RefreshDGV()
        {
            // HACK: JS, Our _allResources does currently not support data binding. Thus we need to improvise
            teachingResourcesDGV.DataSource = null;
            teachingResourcesDGV.Rows.Clear();
            teachingResourcesDGV.DataSource = _allResources;
            teachingResourcesDGV.Refresh();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ClearEntryUIElements();
        }

        private void EnableEntryButtons(object sender, KeyPressEventArgs e)
        {
            createButton.Enabled = true;
            createButton.Text = "Erstellen";
            cancelButton.Enabled = true;
        }

        private void TeachingResourcesDGV_SelectionChanged(object sender, EventArgs e)
        {
            TeachingResource resource = GetCurrentlySelectedResource();
            titelTextBox.Text = resource.Title;
            urlTextBox.Text = resource.Url;
        }

        private TeachingResource GetCurrentlySelectedResource()
        {
            return teachingResourcesDGV.CurrentRow.DataBoundItem as TeachingResource;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            TeachingResource resource = GetCurrentlySelectedResource();
            _allResources.Remove(resource);
            _db.Save(_allResources);
            RefreshDGV();
        }
    }
}
