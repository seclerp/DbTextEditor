using System;
using System.Windows.Forms;

namespace DbTextEditor.Forms.Dialogs
{
    public partial class ImportFileForm : Form
    {
        public ImportFileForm()
        {
            InitializeComponent();
            CancelButton.Click += OnCancelButtonButtonClick;
            ImportButton.Click += OnImportButtonClick;
            ImportFileChoosePath.Click += OnImportFileChoosePathClick;
        }

        public string FromFileName { get; protected set; } = "";
        public string ToFileName { get; protected set; } = "";

        private void OnImportFileChoosePathClick(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK) ImportFileBox.Text = OpenDialog.FileName;
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