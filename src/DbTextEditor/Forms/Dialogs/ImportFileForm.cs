using System;
using System.Windows.Forms;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class ImportFileForm : Form
    {
        public string FromFileName { get; protected set; } = "";
        public string ToFileName { get; protected set; } = "";

        public ImportFileForm()
        {
            InitializeComponent();
            CancelButton.Click += OnCancelButtonButtonClick;
            ImportButton.Click += OnImportButtonClick;
            ImportFileChoosePath.Click += OnImportFileChoosePathClick;
        }

        private void OnImportFileChoosePathClick(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                ImportFileBox.Text = OpenDialog.FileName;
            }
        }

        private void OnImportButtonClick(object sender, EventArgs e)
        {
            FromFileName = ImportFileBox.Text;
            ToFileName = NameInDbBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelButtonButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
