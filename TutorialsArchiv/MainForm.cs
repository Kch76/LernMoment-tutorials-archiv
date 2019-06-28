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
        enum EntryMode
        {
            NoSelection = 0,
            ResourceSelected,
            UserEditsResource
        }

        private readonly FileDatabase _db = null;
        private readonly List<TeachingResource> _allResources = null;
        private EntryMode _mode = EntryMode.NoSelection;
        private TeachingResource _activeResource = null;

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
            RefreshDGV();
        }

        private void EnterNoSelectionMode()
        {
            if (_mode == EntryMode.UserEditsResource)
            {
                throw new InvalidOperationException("Wir befinden uns im Editiermodus. Das darf jetzt nicht sein!");
            }

            _mode = EntryMode.NoSelection;
            _activeResource = null;

            cancelButton.Enabled = false;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;

            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;

            teachingResourcesDGV.Select(); // Der Detailbereich soll nicht den Fokus haben!
        }

        private void TeachingResourcesDGV_SelectionChanged(object sender, EventArgs e)
        {
            EnterSelectionMode(GetCurrentlySelectedResource());
        }

        private void EnterSelectionMode(TeachingResource selectedResource)
        {
            if (_mode == EntryMode.UserEditsResource)
            {
                throw new InvalidOperationException("Wir befinden uns im Editiermodus. Das darf jetzt nicht sein!");
            }

            _mode = EntryMode.ResourceSelected;
            _activeResource = selectedResource;

            deleteButton.Enabled = true;
            cancelButton.Enabled = false;
            updateButton.Enabled = false;

            titelTextBox.Text = _activeResource.Title;
            urlTextBox.Text = _activeResource.Url;

            titelTextBox.Select();
        }

        private void AllTextBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnterEditingMode();
        }

        private void EnterEditingMode()
        {
            if (_mode == EntryMode.UserEditsResource)
            {
                return;
            }
            else if (_activeResource == null)
            {
                MessageBox.Show(this, "Es wurde noch kein Eintrag zum Ändern ausgewählt oder hinzugefügt!");
                EnterNoSelectionMode();
                return;
            }

            _mode = EntryMode.UserEditsResource;

            deleteButton.Enabled = true;
            cancelButton.Enabled = true;
            updateButton.Enabled = true;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (_mode == EntryMode.UserEditsResource)
            {
                MessageBox.Show("Bitte erst die Änderungen speichern oder verwerfen!");
                return;
            }

            _allResources.Add(new TeachingResource("Neue Ressource", "bitte ausfüllen"));
            RefreshDGV();
            SelectLastRowInDGV();
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
