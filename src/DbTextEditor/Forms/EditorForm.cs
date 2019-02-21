using System;
using System.Configuration;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;
using ScintillaNET;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Forms
{
    public partial class EditorForm : DockContent
    {
        private readonly MainForm _mainForm;
        internal readonly IEditorViewModel EditorViewModel;

        private string _currentFileName;

        private int _maxLineNumberCharLength;
        internal ObservableProperty<string> Contents;
        internal ObservableProperty<bool> IsModified;
        internal ObservableProperty<string> Path;
        internal ObservableProperty<StorageType> Storage;

        public EditorForm(IEditorViewModel editorViewModel, MainForm mainForm)
        {
            EditorViewModel = editorViewModel;
            _mainForm = mainForm;
            Load += OnLoad;
            InitializeComponent();
        }

        public void OnLoad(object sender, EventArgs args)
        {
            InitializeTextEditor();

            MakeBindings();
            TextEditor.TextChanged += OnViewTextChanged;
            TextEditor.InsertCheck += OnTextEditorInsertCheck;
            TextEditor.CharAdded += OnTextEditorCharAdded;

            RecalculateLineNumbersWidth();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Alt | Keys.S:
                    _mainForm.SaveAs(EditorViewModel);
                    break;
                case Keys.Control | Keys.S:
                    _mainForm.Save(EditorViewModel);
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MakeBindings()
        {
            Storage = new ObservableProperty<StorageType>(EditorViewModel.Storage, OnStorageChanged);
            Path = new ObservableProperty<string>(EditorViewModel.Path, OnPathChanged);
            Contents = new ObservableProperty<string>(EditorViewModel.Contents, OnContentsChanged);
            IsModified = new ObservableProperty<bool>(EditorViewModel.IsModified, OnIsModifiedChanged);

            Bindings.BindObservables(EditorViewModel.Storage, Storage, BindingMode.OneWay, false);
            Bindings.BindObservables(EditorViewModel.Path, Path, BindingMode.OneWay, false);
            Bindings.BindObservables(EditorViewModel.Contents, Contents, BindingMode.TwoWay, false);
            Bindings.BindObservables(EditorViewModel.IsModified, IsModified, BindingMode.OneWay, false);

            RefreshTabTitle(Path.Value);
            RefreshContents(Contents.Value);
        }

        private void InitializeTextEditor()
        {
            TextEditor.IndentationGuides = IndentView.LookBoth;

            TextEditor.SetProperty("fold", "1");
            TextEditor.SetProperty("fold.compact", "1");
            TextEditor.SetProperty("fold.html", "1");

            TextEditor.Margins[2].Type = MarginType.Symbol;
            TextEditor.Margins[2].Mask = Marker.MaskFolders;
            TextEditor.Margins[2].Sensitive = true;
            TextEditor.Margins[2].Width = 20;

            TextEditor.Margins[3].Type = MarginType.Color;
            TextEditor.Margins[3].Width = 2;
            TextEditor.Margins[3].BackColor = Color.DarkGray;

            TextEditor.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            TextEditor.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            TextEditor.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            TextEditor.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            TextEditor.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            TextEditor.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextEditor.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            TextEditor.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextEditor.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
        }

        private void OnContentsChanged(string newValue)
        {
            RefreshContents(newValue);
        }

        private void OnPathChanged(string newValue)
        {
            RefreshTabTitle(newValue);
        }

        private void OnIsModifiedChanged(bool _)
        {
            RefreshTabTitle(EditorViewModel.Path);
        }

        private void OnStorageChanged(StorageType _)
        {
            RefreshTabTitle(EditorViewModel.Path);
        }

        private void OnViewTextChanged(object sender, EventArgs e)
        {
            EditorViewModel.TextChangedCommand.Execute(TextEditor.Text);
            RecalculateLineNumbersWidth();
        }

        private void RecalculateLineNumbersWidth()
        {
            var currentMaxLineNumberCharLength = TextEditor.Lines.Count.ToString().Length;
            if (currentMaxLineNumberCharLength == _maxLineNumberCharLength)
                return;

            const int padding = 2;
            TextEditor.Margins[0].Width = TextEditor.TextWidth(Style.LineNumber, new string('9',
                                              currentMaxLineNumberCharLength + 1)) + padding;
            _maxLineNumberCharLength = currentMaxLineNumberCharLength;
        }

        private void RefreshContents(string newValue)
        {
            if (newValue != TextEditor.Text) TextEditor.Text = newValue;
        }

        private void RefreshTabTitle(string newPath)
        {
            var newFileName = newPath is null ? "[new file]" : System.IO.Path.GetFileName(EditorViewModel.Path);
            var modifiedStar = EditorViewModel.IsModified ? "*" : string.Empty;
            var prefix = Storage == StorageType.Database ? "[DB] " : string.Empty;
            if (newFileName != _currentFileName)
            {
                _currentFileName = newFileName;

                SetupHighlighting(System.IO.Path.GetExtension(newFileName));
            }

            TabText = $"{prefix} {_currentFileName}{modifiedStar}";
        }

        private void SetupHighlighting(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".json":
                    SetupJsonHighlighting();
                    break;
                case ".xml":
                    SetupXmlHighlighting();
                    break;
                default:
                    SetupDefaults();
                    break;
            }
        }

        private void SetupXmlHighlighting()
        {
            SetupDefaults();
            TextEditor.Lexer = Lexer.Xml;
            TextEditor.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            TextEditor.Styles[Style.Xml.Entity].ForeColor = Color.Red;
            TextEditor.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            TextEditor.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
            TextEditor.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
            TextEditor.Styles[Style.Xml.DoubleString].ForeColor = Color.DeepPink;
            TextEditor.Styles[Style.Xml.SingleString].ForeColor = Color.DeepPink;
        }

        private void SetupJsonHighlighting()
        {
            SetupDefaults();
            TextEditor.Lexer = Lexer.Json;
            TextEditor.Styles[Style.Json.Default].ForeColor = Color.Silver;
            TextEditor.Styles[Style.Json.BlockComment].ForeColor = Color.Green;
            TextEditor.Styles[Style.Json.LineComment].ForeColor = Color.Green;
            TextEditor.Styles[Style.Json.Number].ForeColor = Color.Olive;
            TextEditor.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            TextEditor.Styles[Style.Json.String].ForeColor = Color.Red;
            TextEditor.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            TextEditor.Styles[Style.Json.Operator].ForeColor = Color.Purple;
        }

        private void SetupDefaults()
        {
            TextEditor.StyleResetDefault();
            TextEditor.Styles[Style.Default].Font = ConfigurationManager.AppSettings["Editor.Font.Family"];
            TextEditor.Styles[Style.Default].Size =
                Convert.ToInt32(ConfigurationManager.AppSettings["Editor.Font.Size"]);
            TextEditor.StyleClearAll();
        }

        private void OnTextEditorInsertCheck(object sender, InsertCheckEventArgs e)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                var startPos = TextEditor.Lines[TextEditor.LineFromPosition(TextEditor.CurrentPosition)].Position;
                var endPos = e.Position;
                //Text until the caret so that the whitespace is always equal in every line.
                var curLineText = TextEditor.GetTextRange(startPos, endPos - startPos);

                var indentMatch = Regex.Match(curLineText, "^[ \\t]*");
                e.Text = e.Text + indentMatch.Value;
                if (Regex.IsMatch(curLineText, "{\\s*$")) e.Text = e.Text + "\t";
            }
        }

        private void OnTextEditorCharAdded(object sender, CharAddedEventArgs e)
        {
            //The '}' char.
            if (e.Char == 125)
            {
                var curLine = TextEditor.LineFromPosition(TextEditor.CurrentPosition);

                if (TextEditor.Lines[curLine].Text.Trim() == "}")
                    SetIndent(TextEditor, curLine, GetIndent(TextEditor, curLine) - 4);
            }
        }

        #region CodeIndent Handlers

        private const int SciSetlineindentation = 2126;
        private const int SciGetlineindentation = 2127;

        private void SetIndent(Scintilla scin, int line, int indent)
        {
            scin.DirectMessage(SciSetlineindentation, new IntPtr(line), new IntPtr(indent));
        }

        private int GetIndent(Scintilla scin, int line)
        {
            return scin.DirectMessage(SciGetlineindentation, new IntPtr(line), IntPtr.Zero).ToInt32();
        }

        #endregion
    }
}