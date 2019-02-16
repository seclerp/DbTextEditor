using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using DbTextEditor.Forms;
using DbTextEditor.ViewModel;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Views
{
    public class MainView : IDisposable
    {
        private readonly MainForm _form;
        private readonly MainViewModel _mainViewModel;

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
        }
        private void InitializeFileView()
        {
//            var fileTree = new ExplorerTree
//            {
//                ShowMyNetwork = false,
//                ShowMyFavorites = false
//            };
//            
//            form.MainDockPanel.
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

        public void Dispose()
        {
        }
    }
}