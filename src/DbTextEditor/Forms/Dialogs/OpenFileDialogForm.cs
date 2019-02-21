using System;
using System.Linq;
using System.Windows.Forms;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class OpenFileDialogForm : FileDialogForm
    {
        public OpenFileDialogForm()
        {
            InitializeComponent();

            LocalRadioButton.CheckedChanged += OnChecked;
            OpenButton.Click += OnOpenClick;
            CancelButton.Click += OnCancelButtonOnClick;
            ChoosePathButton.Click += OnChoosePathButtonClick;

            RefreshChecked();
            RefreshFiles();
        }

        private void OnCancelButtonOnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnChoosePathButtonClick(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK) FromFileBox.Text = OpenDialog.FileName;
        }

        private void OnOpenClick(object sender, EventArgs args)
        {
            if (LocalRadioButton.Checked)
            {
                var fileName = FromFileBox.Text.Trim();
                if (!LocalFilesAdapter.Exists(fileName))
                {
                    MessageBox.Show($"File '{fileName}' not exists in local filesystem");
                    return;
                }

                FileName = fileName;
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (DatabaseRadioButton.Checked)
            {
                if (FromDatabaseListView.SelectedItems.Count == 0) return;

                var fileName = FromDatabaseListView.SelectedItems[0].Text;
                if (!DbFilesAdapter.Exists(fileName))
                {
                    MessageBox.Show($"File '{fileName}' not exists in database");
                    return;
                }

                FileName = fileName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void OnChecked(object sender, EventArgs args)
        {
            RefreshChecked();
        }

        private void RefreshChecked()
        {
            if (LocalRadioButton.Checked)
            {
                FromDatabaseListView.Enabled = false;
                FromFileBox.Enabled = true;
                ChoosePathButton.Enabled = true;
                StorageType = StorageType.Local;
            }
            else if (DatabaseRadioButton.Checked)
            {
                FromDatabaseListView.Enabled = true;
                FromFileBox.Enabled = false;
                ChoosePathButton.Enabled = false;
                StorageType = StorageType.Database;
            }
        }

        private void RefreshFiles()
        {
            DbViewModel.RefreshCommand.Execute();
            var listViewItems = DbViewModel.DbFileNames
                .Select(fileName => new ListViewItem(fileName, 0))
                .ToArray();

            FromDatabaseListView.Items.Clear();
            FromDatabaseListView.Items.AddRange(listViewItems);
        }
    }
}