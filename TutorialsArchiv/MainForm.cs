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
        enum UiMode
        {
            Init = 0,
            UserSelectedExistingResource,
            UserEditsExistingResource,
            UserEditsNewResource
        }

        private readonly FileDatabase _db = null;
        private readonly List<TeachingResource> _allResources = null;
        private UiMode _mode = UiMode.Init;
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
            EnterInitMode();
            RefreshDGV();
        }

        private void EnterInitMode()
        {
            _mode = UiMode.Init;
            _activeResource = null;

            cancelButton.Enabled = false;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;
            createButton.Enabled = true;

            titelTextBox.Text = string.Empty;
            urlTextBox.Text = string.Empty;

            //teachingResourcesDGV.Select(); // Der Detailbereich soll nicht den Fokus haben!
        }

        private void EnterSelectExistingMode(TeachingResource selectedResource)
        {
            _mode = UiMode.UserSelectedExistingResource;
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
            // editing a new Resource is different than an editing an existing Resource
            if (_mode != UiMode.UserEditsNewResource)
            {
                EnterEditExistingMode();
            }
        }

        private void EnterEditExistingMode()
        {
            if (_mode == UiMode.UserEditsExistingResource)
            {
                return;
            }
            else if (_activeResource == null)
            {
                MessageBox.Show(this, "Es wurde noch kein Eintrag zum Ändern ausgewählt!");
                EnterInitMode();
                return;
            }

            _mode = UiMode.UserEditsExistingResource;

            deleteButton.Enabled = true;
            cancelButton.Enabled = true;
            updateButton.Enabled = true;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (_mode == UiMode.UserEditsExistingResource)
            {
                MessageBox.Show("Bitte erst die Änderungen speichern oder verwerfen!");
                return;
            }

            TeachingResource newResource = new TeachingResource("Neue Ressource", "bitte ausfüllen");
            _allResources.Add(newResource);
            RefreshDGV();
            SelectLastRowInDGV();

            EnterEditNewMode(newResource);
        }

        private void EnterEditNewMode(TeachingResource newResource)
        {
            if (_mode == UiMode.UserEditsExistingResource)
            {
                MessageBox.Show("Bitte erst die Änderungen speichern oder verwerfen!");
                return;
            }

            _mode = UiMode.UserEditsNewResource;

            _activeResource = newResource;

            createButton.Enabled = false;
            deleteButton.Enabled = false;
            cancelButton.Enabled = true;
            updateButton.Enabled = true;

            titelTextBox.Text = _activeResource.Title;
            urlTextBox.Text = _activeResource.Url;

            titelTextBox.Select();
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
            teachingResourcesDGV.RowEnter -= new DataGridViewCellEventHandler(TeachingResourcesDGV_RowEnter);
            teachingResourcesDGV.ClearSelection();
            teachingResourcesDGV.DataSource = null;
            teachingResourcesDGV.Rows.Clear();
            teachingResourcesDGV.DataSource = _allResources;
            teachingResourcesDGV.Refresh();
            teachingResourcesDGV.RowEnter += new DataGridViewCellEventHandler(TeachingResourcesDGV_RowEnter);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (_mode == UiMode.UserEditsNewResource)
            {
                // TODO: JS, should we ask user whether he is sure to delete the input?
                _allResources.RemoveAt(_allResources.Count - 1);
                RefreshDGV();

                _activeResource = null;
                _mode = UiMode.Init;
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException("UI ist nicht im Edit-Modus und somit kann dieser auch nicht angebrochen werden!");
            }
        }

        private TeachingResource GetCurrentlySelectedResource()
        {
            return teachingResourcesDGV.CurrentRow.DataBoundItem as TeachingResource;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_mode == UiMode.UserSelectedExistingResource)
            {
                TeachingResource resource = GetCurrentlySelectedResource();
                _allResources.Remove(resource);
                _db.Save(_allResources);
                RefreshDGV();
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException("UI ist nicht im Resource-Ausgewählt-Modus! Nur darin kann gelöscht werden!");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (_mode == UiMode.UserEditsNewResource || _mode == UiMode.UserEditsExistingResource)
            {
                _activeResource.Title = titelTextBox.Text;
                _activeResource.Url = urlTextBox.Text;
                _db.Save(_allResources);
                RefreshDGV();
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException("UI ist nicht im Edit-Modus und somit kann dieser auch nicht angebrochen werden!");
            }
        }

        private void TeachingResourcesDGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TeachingResource selectedResource = teachingResourcesDGV.Rows[e.RowIndex].DataBoundItem as TeachingResource;
            EnterSelectExistingMode(selectedResource);
        }
    }
}
