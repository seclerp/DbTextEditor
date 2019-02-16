using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using DbTextEditor.ViewModel;
using DbTextEditor.Views;
using ScintillaNET;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Forms
{
    public partial class EditorForm : DockContent
    {
        private readonly EditorView _editorView;
        internal Scintilla TextEditor;

        public EditorForm(EditorViewModel editorViewModel)
        {
            Load += OnLoad;
            InitializeComponent();

            _editorView = new EditorView(this, editorViewModel);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _editorView.OnLoad();
        }
    }
}
