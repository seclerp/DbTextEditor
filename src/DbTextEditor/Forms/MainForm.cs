using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using DbTextEditor.Forms.Dialogs;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Forms
{
    public partial class MainForm : Form
    {
        private readonly IMainViewModel _mainViewModel;
        private readonly ObservableProperty<IEditorViewModel> _selectedEditor = new ObservableProperty<IEditorViewModel>();

        public MainForm()
        {
            _mainViewModel = CompositionRoot.Resolve<IMainViewModel>();
            InitializeComponent();
            InitializeMainMenu();
            InitializeDockPanel();
            InitializeDatabaseView();
            MakeBindings();
        }

        // This is to ignore saving through hotkeys on main form 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Alt | Keys.S:
                case Keys.Control | Keys.S:
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MakeBindings()
        {
            Bindings.ForCollection(_mainViewModel.OpenedEditors, OnOpenedEditorCollectionChanged);
            Bindings.BindObservables(_mainViewModel.SelectedEditor, _selectedEditor);
        }

        private void InitializeMainMenu()
        {
            // File
            // 
            var file = new ToolStripMenuItem("File");
            var newFile = new ToolStripMenuItem("New");
            newFile.Click += OnNewFileClick;
            newFile.ShortcutKeys = Keys.Control | Keys.N;

            var openFile = new ToolStripMenuItem("Open");
            openFile.ShortcutKeys = Keys.Control | Keys.O;
            openFile.Click += OnOpenFileClick;

            var saveFile = new ToolStripMenuItem("Save");
            saveFile.ShortcutKeys = Keys.Control | Keys.S;
            saveFile.Click += OnSaveFileClick;

            var saveFileAs = new ToolStripMenuItem("Save as");
            saveFileAs.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            saveFileAs.Click += OnSaveFileAsClick;

            var saveAll = new ToolStripMenuItem("Save all");
            saveAll.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;

            var exit = new ToolStripMenuItem("Exit");
            exit.ShortcutKeys = Keys.Alt | Keys.F4;

            file.DropDownItems.AddRange(new ToolStripItem[]
            {
                newFile, openFile, saveFile, saveFileAs, saveAll,
                new ToolStripSeparator(),
                exit
            });
            MainMenu.Items.Add(file);

            // Database
            //
            var database = new ToolStripMenuItem("Database");

            var import = new ToolStripMenuItem("Import file");
            import.ShortcutKeys = Keys.Control | Keys.Shift | Keys.I;
            import.Click += OnImportClick;

            var export = new ToolStripMenuItem("Export file");
            export.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
            export.Click += OnExportClick;

            database.DropDownItems.AddRange(new ToolStripItem[]
            {
                import, export
            });
            MainMenu.Items.Add(database);
        }

        private void OnImportClick(object sender, EventArgs e)
        {
            using (var dialog = new ImportFileForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _mainViewModel.ImportCommand.Execute((dialog.FromFileName, dialog.ToFileName));
                }
            }
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            using (var dialog = new ExportFileForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _mainViewModel.ExportCommand.Execute((dialog.FromFileName, dialog.ToFileName));
                }
            }
        }

        private void InitializeDockPanel()
        {
            MainDockPanel.ActiveContentChanged +=
                OnActiveContentChanged;
        }

        private void InitializeDatabaseView()
        {
            new DatabaseViewForm().Show(MainDockPanel, DockState.DockLeft);
        }

        private void OnActiveContentChanged(object sender, EventArgs args)
        {
            if (MainDockPanel.ActiveContent is EditorForm editorForm)
            {
                _selectedEditor.Value = editorForm.EditorViewModel;
            }
        }

        private void OnOpenedEditorCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in args.NewItems)
                {
                    var editorForm = new EditorForm(newItem as IEditorViewModel, this);
                    editorForm.Closing += OnEditorFormClosing;
                    editorForm.DockAreas = DockAreas.Document | DockAreas.DockTop | DockAreas.DockBottom |
                                           DockAreas.DockLeft | DockAreas.DockRight;
                    editorForm.Show(MainDockPanel, DockState.Document);
                }
            }
        }

        private void OnNewFileClick(object sender, EventArgs args)
        {
            _mainViewModel.NewFileCommand.Execute();
        }

        private void OnOpenFileClick(object sender, EventArgs args)
        {
            using (var openDialog = new OpenFileDialogForm())
            {
                if (openDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _mainViewModel.OpenFileCommand.Execute((openDialog.FileName, openDialog.StorageType));
            }
        }

        private void OnSaveFileClick(object sender, EventArgs args)
        {
            Save(_selectedEditor.Value);
        }

        private void OnSaveFileAsClick(object sender, EventArgs e)
        {
            SaveAs(_selectedEditor.Value);
        }

        private void OnEditorFormClosing(object sender, CancelEventArgs e)
        {
            var editorForm = sender as EditorForm;
            if (editorForm?.IsModified)
            {
                var saveQuestionResult = MessageBox.Show(
                    $"Do you want to save changed made in '{editorForm.Path}'?",
                    "Save changes?", MessageBoxButtons.YesNoCancel);
                if (saveQuestionResult == DialogResult.Yes)
                {
                    if (!Save(editorForm.EditorViewModel))
                    {
                        e.Cancel = true;
                    }
                }
                else if (saveQuestionResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        public bool Save(IEditorViewModel editor)
        {
            if (editor is null)
            {
                return false;
            }

            if (editor.IsNewFile)
            {
                using (var dialog = new SaveFileDialogForm())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        editor.SaveFileAsCommand.Execute((dialog.FileName, dialog.StorageType));
                        return true;
                    }
                    return false;
                }
            }
            editor.SaveFileCommand.Execute();
            return true;
        }

        public void SaveAs(IEditorViewModel editor)
        {
            if (editor is null)
            {
                return;
            }

            using (var dialog = new SaveFileDialogForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    editor.SaveFileAsCommand.Execute((dialog.FileName, dialog.StorageType));
                }
            }
        }
    }
}
