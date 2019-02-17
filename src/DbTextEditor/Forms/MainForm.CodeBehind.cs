using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using DbTextEditor.Shared;
using DbTextEditor.ViewModel;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Forms
{
    public partial class MainForm
    {
        private readonly MainViewModel _mainViewModel;
        private EditorForm _selectedEditor;

        public static SaveFileDialog SaveDialog { get; private set; }
        public static OpenFileDialog OpenDialog { get; private set; }

        public MainForm()
        {
            _mainViewModel = new MainViewModel();
            InitializeComponent();
            SetupDialogs();
            InitializeMainMenu();
            InitializeDockPanel();
            MakeDataBindings();
        }

        private void MakeDataBindings()
        {
            Bindings.MakeForCollection(_mainViewModel.OpenedEditors, OnOpenedEditorCollectionChanged);
        }

        private void SetupDialogs()
        {
            SaveDialog = MainSaveFileDialog;
            OpenDialog = MainOpenFileDialog;
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
            MainMenu.Items.Add(file);
        }

        private void InitializeDockPanel()
        {
            MainDockPanel.ActiveDocumentChanged +=
                (sender, args) => _selectedEditor = MainDockPanel.ActiveDocument as EditorForm;
        }

        private void OnOpenedEditorCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in args.NewItems)
                {
                    var editorForm = new EditorForm(newItem as EditorViewModel);
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
            if (MainOpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var fileName in MainOpenFileDialog.FileNames)
            {
                _mainViewModel.OpenFileCommand.Execute(fileName);
            }
        }

        private void OnSaveFileClick(object sender, EventArgs args)
        {
            _selectedEditor?.Save();
        }
    }
}