using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TutorialsArchiv.UiExtensions;

namespace TutorialsArchiv
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public delegate void TeachingResourceHandler(TeachingResource resource);
        public delegate void CloseFormHandler(object sender, CloseRequestedEventArgs args);

        public event TeachingResourceHandler ResourceSelected;
        public event EventHandler ResourceEdited;
        public event EventHandler ResourceEditCompleted;
        public event EventHandler ResourceCreationRequested;
        public event EventHandler ResourceDeletionRequested;
        public event EventHandler Canceled;
        public event CloseFormHandler FormCloseRequested;

        public TeachingResource CurrentResource { get; private set; }

        public void EnterNoResourcesMode()
        {
            cancelButton.Enabled = false;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;
            createButton.Enabled = true;

            titelTextBox.Text = string.Empty;
            titelTextBox.Enabled = false;
            urlTextBox.Text = string.Empty;
            urlTextBox.Enabled = false;

            teachingResourcesDGV.Visible = false;
        }

        public void EnterFirstResourceEditMode()
        {
            titelTextBox.Enabled = true;
            urlTextBox.Enabled = true;
        }

        public void LeaveNoResourcesMode()
        {
            EnterFirstResourceEditMode();

            teachingResourcesDGV.Visible = true;
        }

        public void EnterInitMode()
        {
            cancelButton.Enabled = false;
            deleteButton.Enabled = false;
            updateButton.Enabled = false;
            createButton.Enabled = true;

            titelTextBox.Text = string.Empty;
            titelTextBox.Enabled = false;
            urlTextBox.Text = string.Empty;
            urlTextBox.Enabled = false;

            teachingResourcesDGV.Enable();
        }

        public void EnterSelectExistingMode(TeachingResource selectedResource)
        {
            deleteButton.Enabled = true;
            cancelButton.Enabled = false;
            updateButton.Enabled = false;

            CurrentResource = selectedResource;

            titelTextBox.Text = selectedResource.Title;
            titelTextBox.Enabled = true;
            urlTextBox.Text = selectedResource.Url;
            urlTextBox.Enabled = true;

            titelTextBox.Select();
        }

        public void EnterEditExistingMode()
        {
            deleteButton.Enabled = false;
            createButton.Enabled = false;
            cancelButton.Enabled = true;
            updateButton.Enabled = true;

            teachingResourcesDGV.Disable();
        }

        public void EnterEditNewMode(TeachingResource newResource)
        {
            createButton.Enabled = false;
            deleteButton.Enabled = false;
            cancelButton.Enabled = true;
            updateButton.Enabled = true;

            CurrentResource = newResource;
            titelTextBox.Text = newResource.Title;
            urlTextBox.Text = newResource.Url;

            titelTextBox.Select();
        }

        public void HighlightLatestResource()
        {
            teachingResourcesDGV.CurrentCell = teachingResourcesDGV.Rows[teachingResourcesDGV.Rows.Count - 1].Cells[0];
        }

        public void UpdateResourceCollectionView(IEnumerable<TeachingResource> resources)
        {
            // HACK: JS, Our _allResources does currently not support data binding. Thus we need to improvise
            teachingResourcesDGV.RowEnter -= new DataGridViewCellEventHandler(TeachingResourcesDGV_RowEnter);
            teachingResourcesDGV.ClearSelection();
            teachingResourcesDGV.DataSource = null;
            teachingResourcesDGV.Rows.Clear();
            teachingResourcesDGV.DataSource = resources;
            teachingResourcesDGV.Refresh();
            teachingResourcesDGV.RowEnter += new DataGridViewCellEventHandler(TeachingResourcesDGV_RowEnter);
        }

        public void ShowMessageToUser(string message)
        {
            MessageBox.Show(this, message);
        }

        public bool ShowOkCancelMessageToUser(string message)
        {
            DialogResult result = MessageBox.Show(this, message, "Frage", MessageBoxButtons.OKCancel);
            return result == DialogResult.OK;
        }

        private void RaiseEventWithEmptyArgs(EventHandler handler)
        {
            EventHandler theHandler = handler;
            theHandler?.Invoke(this, EventArgs.Empty);
        }

        private void AllTextBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            RaiseEventWithEmptyArgs(ResourceEdited);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            RaiseEventWithEmptyArgs(ResourceCreationRequested);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            RaiseEventWithEmptyArgs(Canceled);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            RaiseEventWithEmptyArgs(ResourceDeletionRequested);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CurrentResource.Title = titelTextBox.Text;
            CurrentResource.Url = urlTextBox.Text;

            RaiseEventWithEmptyArgs(ResourceEditCompleted);
        }

        private void TeachingResourcesDGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TeachingResource selected = teachingResourcesDGV.Rows[e.RowIndex].DataBoundItem as TeachingResource;

            TeachingResourceHandler handler = ResourceSelected;
            handler?.Invoke(selected);
        }

        private void TitelTextBox_Validating(object sender, CancelEventArgs e)
        {
            // TODO: JS, should validation go into Model?
            if (titelTextBox.Text.Contains(';'))
            {
                e.Cancel = true;
                int index = titelTextBox.Text.IndexOf(';');
                if (index < titelTextBox.Text.Length)
                {
                    titelTextBox.Select(index, 1);
                }
                else
                {
                    titelTextBox.Select(index - 1, 1);
                }

                errorProvider1.SetError(titelTextBox, "Der Titel darf kein Semikolon (;) enthalten!");
            }
        }

        private void TitelTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(titelTextBox, "");
        }

        private void UrlTextBox_Validating(object sender, CancelEventArgs e)
        {
            bool isUrlValid = Regex.IsMatch(urlTextBox.Text, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", RegexOptions.IgnoreCase);

            if (urlTextBox.Enabled && !isUrlValid)
            {
                // Cancel following events e.g. Validated
                e.Cancel = true;

                errorProvider1.SetError(urlTextBox, "Es sind nur gültige URLs mit http oder https am Anfang erlaubt!");
            }
        }

        private void UrlTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(urlTextBox, "");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseFormHandler handler = FormCloseRequested;
            CloseRequestedEventArgs args = new CloseRequestedEventArgs();
            handler?.Invoke(this, args);

            if (args.ForceClose)
            {
                // close even if validation error exists
                e.Cancel = false;
            }
            else
            {
                // seems like user likes to save changes first!
                e.Cancel = true;
            }
        }

    }
}
