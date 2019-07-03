namespace TutorialsArchiv
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.urlLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.titelTextBox = new System.Windows.Forms.TextBox();
            this.titelLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.teachingResourcesDGV = new System.Windows.Forms.DataGridView();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.emptyDgvLabel = new System.Windows.Forms.Label();
            this.resourceEntryErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mediumLabel = new System.Windows.Forms.Label();
            this.mediumComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.teachingResourcesDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourceEntryErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(331, 63);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(32, 13);
            this.urlLabel.TabIndex = 0;
            this.urlLabel.Text = "URL:";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(378, 61);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(323, 20);
            this.urlTextBox.TabIndex = 3;
            this.urlTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllTextBoxes_KeyPress);
            this.urlTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.UrlTextBox_Validating);
            this.urlTextBox.Validated += new System.EventHandler(this.UrlTextBox_Validated);
            // 
            // titelTextBox
            // 
            this.titelTextBox.Location = new System.Drawing.Point(378, 35);
            this.titelTextBox.Name = "titelTextBox";
            this.titelTextBox.Size = new System.Drawing.Size(323, 20);
            this.titelTextBox.TabIndex = 1;
            this.titelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllTextBoxes_KeyPress);
            this.titelTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.TitelTextBox_Validating);
            this.titelTextBox.Validated += new System.EventHandler(this.TitelTextBox_Validated);
            // 
            // titelLabel
            // 
            this.titelLabel.AutoSize = true;
            this.titelLabel.Location = new System.Drawing.Point(331, 37);
            this.titelLabel.Name = "titelLabel";
            this.titelLabel.Size = new System.Drawing.Size(30, 13);
            this.titelLabel.TabIndex = 2;
            this.titelLabel.Text = "Titel:";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(12, 343);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(90, 23);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Hinzufügen";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(536, 306);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Verwerfen";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // teachingResourcesDGV
            // 
            this.teachingResourcesDGV.AllowUserToAddRows = false;
            this.teachingResourcesDGV.AllowUserToDeleteRows = false;
            this.teachingResourcesDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.teachingResourcesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teachingResourcesDGV.Location = new System.Drawing.Point(12, 11);
            this.teachingResourcesDGV.Name = "teachingResourcesDGV";
            this.teachingResourcesDGV.ReadOnly = true;
            this.teachingResourcesDGV.RowHeadersVisible = false;
            this.teachingResourcesDGV.RowHeadersWidth = 82;
            this.teachingResourcesDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.teachingResourcesDGV.Size = new System.Drawing.Size(286, 317);
            this.teachingResourcesDGV.TabIndex = 6;
            this.teachingResourcesDGV.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.TeachingResourcesDGV_RowEnter);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(369, 306);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "Entfernen";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(617, 306);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "Ändern";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // emptyDgvLabel
            // 
            this.emptyDgvLabel.BackColor = System.Drawing.Color.Silver;
            this.emptyDgvLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.emptyDgvLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emptyDgvLabel.Location = new System.Drawing.Point(62, 119);
            this.emptyDgvLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.emptyDgvLabel.Name = "emptyDgvLabel";
            this.emptyDgvLabel.Size = new System.Drawing.Size(148, 97);
            this.emptyDgvLabel.TabIndex = 9;
            this.emptyDgvLabel.Text = "Füge deine erste Quelle (Lernmaterial) mithilfe des \"Hinzufügen\" Buttons (unten l" +
    "inks) ein!";
            // 
            // resourceEntryErrorProvider
            // 
            this.resourceEntryErrorProvider.ContainerControl = this;
            // 
            // mediumLabel
            // 
            this.mediumLabel.AutoSize = true;
            this.mediumLabel.Location = new System.Drawing.Point(331, 89);
            this.mediumLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mediumLabel.Name = "mediumLabel";
            this.mediumLabel.Size = new System.Drawing.Size(47, 13);
            this.mediumLabel.TabIndex = 10;
            this.mediumLabel.Text = "Medium:";
            // 
            // mediumComboBox
            // 
            this.mediumComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mediumComboBox.FormattingEnabled = true;
            this.mediumComboBox.Location = new System.Drawing.Point(378, 88);
            this.mediumComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mediumComboBox.Name = "mediumComboBox";
            this.mediumComboBox.Size = new System.Drawing.Size(182, 21);
            this.mediumComboBox.TabIndex = 11;
            this.mediumComboBox.SelectionChangeCommitted += new System.EventHandler(this.MediumComboBox_SelectionChangeCommitted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(721, 378);
            this.Controls.Add(this.mediumComboBox);
            this.Controls.Add(this.mediumLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.teachingResourcesDGV);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.titelTextBox);
            this.Controls.Add(this.titelLabel);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.emptyDgvLabel);
            this.Name = "MainForm";
            this.Text = "Lernmaterial Finder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.teachingResourcesDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourceEntryErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.TextBox titelTextBox;
        private System.Windows.Forms.Label titelLabel;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView teachingResourcesDGV;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label emptyDgvLabel;
        private System.Windows.Forms.ErrorProvider resourceEntryErrorProvider;
        private System.Windows.Forms.ComboBox mediumComboBox;
        private System.Windows.Forms.Label mediumLabel;
    }
}

