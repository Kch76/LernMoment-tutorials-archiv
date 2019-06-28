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
        private bool _isUserEditing = false;

        public MainForm()
        {
            InitializeComponent();
            _allResources = new List<TeachingResource>();
            _db = new FileDatabase("file-database.csv");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _allResources.AddRange(_db.LoadAllEntries());
            EnterNoSelectionMode();
        }

        private void EnterNoSelectionMode()
        {
            if (_isUserEditing)
            {
                throw new InvalidOperationException("Wir befinden uns im Editiermodus. Das darf jetzt nicht sein!");
            }

            cancelButton.Enabled = false;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;

            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;

            RefreshDGV();
            teachingResourcesDGV.Select(); // Der Detailbereich soll nicht den Fokus haben!
        }

        private void TeachingResourcesDGV_SelectionChanged(object sender, EventArgs e)
        {
            TeachingResource resource = GetCurrentlySelectedResource();
            EnterSelectionMode(resource);
        }

        private void EnterSelectionMode(TeachingResource selectedResource)
        {
            if (_isUserEditing)
            {
                throw new InvalidOperationException("Wir befinden uns im Editiermodus. Das darf jetzt nicht sein!");
            }

            deleteButton.Enabled = true;
            cancelButton.Enabled = false;
            updateButton.Enabled = false;

            titelTextBox.Text = selectedResource.Title;
            urlTextBox.Text = selectedResource.Url;

            RefreshDGV();
            titelTextBox.Select();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (_isUserEditing)
            {
                MessageBox.Show("Bitte erst die Änderungen speichern oder verwerfen!");
                return;
            }

            _allResources.Add(new TeachingResource("Neue Ressource", "bitte ausfüllen"));
            RefreshDGV();
            SelectLastRowInDGV();
            ClearEntryUIElements();
            _isUserEditing = true;
        }

        private void SelectLastRowInDGV()
        {
            teachingResourcesDGV.CurrentCell = teachingResourcesDGV.Rows[teachingResourcesDGV.Rows.Count - 1].Cells[0];
        }

        private void ClearEntryUIElements()
        {
            cancelButton.Enabled = false;
            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;
        }

        private void RefreshDGV()
        {
            // HACK: JS, Our _allResources does currently not support data binding. Thus we need to improvise
            teachingResourcesDGV.SelectionChanged -= new EventHandler(TeachingResourcesDGV_SelectionChanged);
            teachingResourcesDGV.DataSource = null;
            teachingResourcesDGV.Rows.Clear();
            teachingResourcesDGV.DataSource = _allResources;
            teachingResourcesDGV.Refresh();
            teachingResourcesDGV.ClearSelection();
            teachingResourcesDGV.SelectionChanged += new EventHandler(TeachingResourcesDGV_SelectionChanged);
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
            ClearEntryUIElements();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            TeachingResource resource = GetCurrentlySelectedResource();
            resource.Title = titelTextBox.Text;
            resource.Url = urlTextBox.Text;
            _db.Save(_allResources);
            RefreshDGV();
        }
    }
}
