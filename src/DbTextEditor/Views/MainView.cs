using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using WindowsExplorer;
using DbTextEditor.Forms;
using DbTextEditor.ViewModel;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Views
{
    public class MainView : IDisposable
    {
        private readonly MainForm _form;
        private readonly MainViewModel _mainViewModel;
        private EditorForm _selectedEditor;

        public MainView(MainForm form)
        {
            _form = form;
            _mainViewModel = new MainViewModel();
            InitializeMainMenu();
            InitializeDockPanel();
            InitializeFileView();
        }

        private void InitializeMainMenu()
        {
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

            var saveAll = new ToolStripMenuItem("Save all");
            saveAll.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;

            var close = new ToolStripMenuItem("Close");
            var exit = new ToolStripMenuItem("Exit");
            exit.ShortcutKeys = Keys.Alt | Keys.F4;

            file.DropDownItems.AddRange(new ToolStripItem[]
            {
                newFile, openFile, saveFile, saveAll,
                new ToolStripSeparator(),
                close, exit
            });
            _form.MainMenu.Items.Add(file);
        }

        private void InitializeDockPanel()
        {
            _mainViewModel.OpenedEditors.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var editorForm = new EditorForm(args.NewItems[0] as EditorViewModel);
                    editorForm.Show(_form.MainDockPanel, DockState.Document);
                }
            };

            _form.MainDockPanel.ActiveDocumentChanged +=
                (sender, args) => _selectedEditor = _form.MainDockPanel.ActiveDocument as EditorForm;
        }
        private void InitializeFileView()
        {
            var fileTree = new FileTreeForm(this);
            fileTree.Show(_form.MainDockPanel, DockState.DockLeft);
        }

        private void OnNewFileClick(object sender, EventArgs args)
        {
            _mainViewModel.NewFileCommand.Execute();
        }

        private void OnOpenFileClick(object sender, EventArgs args)
        {
            if (_form.MainOpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var fileName in _form.MainOpenFileDialog.FileNames)
            {
                _mainViewModel.OpenFileCommand.Execute(fileName);
            }
        }

        private void OnSaveFileClick(object sender, EventArgs args)
        {
            _selectedEditor?.Save(_form.MainSaveFileDialog);
        }

        public void Dispose()
        {
        }
    }
}