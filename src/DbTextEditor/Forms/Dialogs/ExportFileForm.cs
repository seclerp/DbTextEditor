using System;
using System.Windows.Forms;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class ExportFileForm : Form
    {
        protected IDatabaseViewViewModel DbViewModel;
        protected IFilesAdapter LocalFilesAdapter;

        public ExportFileForm()
        {
            LocalFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
            DbViewModel = CompositionRoot.Resolve<IDatabaseViewViewModel>();

            InitializeComponent();
            CancelButton.Click += OnCancelButtonClick;
            ExportButton.Click += OnExportButtonClick;
            ChoosePathButton.Click += OnChoosePathButtonClick;

            MakeBindings();
            DbViewModel.RefreshCommand.Execute();
        }

        public string FromFileName { get; protected set; } = "";
        public string ToFileName { get; protected set; } = "";

        private void OnChoosePathButtonClick(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK) ToFileName = SaveDialog.FileName;
        }

        private void OnExportButtonClick(object sender, EventArgs e)
        {
            if (FilesInDb.SelectedItem is null) return;
            var fromFileName = (string) FilesInDb.SelectedItem;
            var toFileName = ToFileNameBox.Text.Trim();
            if (LocalFilesAdapter.Exists(toFileName))
            {
                var saveQuestionResult = MessageBox.Show(
                    $"File '{toFileName}' exists. Overwrite it?",
                    "Overwrite", MessageBoxButtons.YesNoCancel);
                switch (saveQuestionResult)
                {
                    case DialogResult.Yes:
                        FromFileName = fromFileName;
                        ToFileName = toFileName;
                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    case DialogResult.Cancel:
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                }
            }

            FromFileName = fromFileName;
            ToFileName = toFileName;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void MakeBindings()
        {
            Bindings.ForCollection(DbViewModel.DbFileNames, args =>
            {
                FilesInDb.Items.Clear();
                foreach (var dbFileName in DbViewModel.DbFileNames) FilesInDb.Items.Add(dbFileName);
            });
        }
    }
}