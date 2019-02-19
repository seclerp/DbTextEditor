using System;
using System.Linq;
using System.Windows.Forms;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class OpenFileDialogForm
    {
        public string FileName { get; private set; } = "";

        private IFilesAdapter _localFilesAdapter;
        private IFilesAdapter _dbFilesAdapter;
        private IDatabaseViewViewModel _dbViewModel;

        public OpenFileDialogForm()
        {
            _localFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
            _dbFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("DbFilesAdapter");
            _dbViewModel = CompositionRoot.Resolve<IDatabaseViewViewModel>();
            InitializeComponent();
            LocalRadioButton.CheckedChanged += OnChecked;
            OpenButton.Click += OnOpenClick;
            ChoosePathButton.Click += OnChoosePathButtonClick;
            RefreshChecked();
            RefreshFiles();
        }

        private void OnChoosePathButtonClick(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                FromFileBox.Text = OpenDialog.FileName;
            }
        }

        private void OnChecked(object sender, EventArgs args)
        {
            RefreshChecked();
        }

        private void OnOpenClick(object sender, EventArgs args)
        {
            if (LocalRadioButton.Checked)
            {
                var fileName = FromFileBox.Text.Trim();
                if (!_localFilesAdapter.Exists(fileName))
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
                var fileName = FromFileBox.Text.Trim();
                if (!_dbFilesAdapter.Exists(fileName))
                {
                    MessageBox.Show($"File '{fileName}' not exists in database");
                    return;
                }

                FileName = fileName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void RefreshChecked()
        {
            if (LocalRadioButton.Checked)
            {
                FromDatabaseListView.Enabled = false;
                FromFileBox.Enabled = true;
                ChoosePathButton.Enabled = true;
            }
            else if (DatabaseRadioButton.Checked)
            {
                FromDatabaseListView.Enabled = true;
                FromFileBox.Enabled = false;
                ChoosePathButton.Enabled = false;
            }
        }

        private void RefreshFiles()
        {
            var filterText = FromDatabaseListView.Text?.Trim() ?? string.Empty;
            var listViewItems = _dbViewModel.DbFileNames
                .Select(fileName => new ListViewItem(fileName, 0))
                .ToArray();

            FromDatabaseListView.Items.Clear();
            FromDatabaseListView.Items.AddRange(listViewItems);
        }
    }
}