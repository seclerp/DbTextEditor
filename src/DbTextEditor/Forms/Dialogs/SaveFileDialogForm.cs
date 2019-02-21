using System;
using System.Windows.Forms;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class SaveFileDialogForm : FileDialogForm
    {
        public SaveFileDialogForm()
        {
            InitializeComponent();
            ToLocalRadioButton.CheckedChanged += OnChecked;
            ToDbRadioButton.CheckedChanged += OnChecked;
            ToLocalChoosePathButton.Click += OnChoosePathButtonClick;
            SaveButton.Click += OnSaveClick;
            CancelButton.Click += OnCancelButtonOnClick;
            RefreshChecked();
        }

        private void OnChoosePathButtonClick(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK) ToLocalFileName.Text = SaveDialog.FileName;
        }

        private void OnChecked(object sender, EventArgs args)
        {
            RefreshChecked();
        }

        private void OnSaveClick(object sender, EventArgs args)
        {
            var fileName = string.Empty;
            if (ToLocalRadioButton.Checked)
                fileName = ToLocalFileName.Text.Trim();
            else if (ToDbRadioButton.Checked) fileName = ToDbFileName.Text.Trim();

            var adapter = StorageType == StorageType.Local ? LocalFilesAdapter : DbFilesAdapter;
            if (adapter.Exists(fileName))
            {
                var saveQuestionResult = MessageBox.Show(
                    $"File '{fileName}' exists. Overwrite it?",
                    "Overwrite", MessageBoxButtons.YesNoCancel);
                switch (saveQuestionResult)
                {
                    case DialogResult.Yes:
                        FileName = fileName;
                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    case DialogResult.Cancel:
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                }
            }

            FileName = fileName;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelButtonOnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefreshChecked()
        {
            if (ToLocalRadioButton.Checked)
            {
                ToDbFileName.Enabled = false;
                ToLocalFileName.Enabled = true;
                ToLocalChoosePathButton.Enabled = true;
                StorageType = StorageType.Local;
            }
            else if (ToDbRadioButton.Checked)
            {
                ToDbFileName.Enabled = true;
                ToLocalFileName.Enabled = false;
                ToLocalChoosePathButton.Enabled = false;
                StorageType = StorageType.Database;
            }
        }
    }
}