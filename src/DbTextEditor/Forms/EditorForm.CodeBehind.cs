﻿using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DbTextEditor.Shared;
using DbTextEditor.ViewModel;
using ScintillaNET;

namespace DbTextEditor.Forms
{
    public partial class EditorForm
    {
        private readonly EditorViewModel _editorViewModel;

        private string _currentFileName;
        private ObservableProperty<string> _path;
        private ObservableProperty<string> _text;
        private ObservableProperty<bool> _isModified;

        public EditorForm(EditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
            Load += OnLoad;
            Closing += OnClosing;
            InitializeComponent();
        }

        public void OnLoad(object sender, EventArgs args)
        {
            InitializeTextEditor();

            MakeBindings();
            TextEditor.TextChanged += OnViewTextChanged;
            TextEditor.InsertCheck += OnTextEditorInsertCheck;
            TextEditor.CharAdded += OnTextEditorCharAdded;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (_isModified.Value)
            {
                var saveQuestionResult = MessageBox.Show(
                    $"Do you want to save changed made in '{_currentFileName}'?", 
                    "Save changes?", MessageBoxButtons.YesNoCancel);
                if (saveQuestionResult == DialogResult.Yes)
                {
                    if (!Save())
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

        public bool Save()
        {
            var path = _editorViewModel.Path.Value;
            if (_editorViewModel.IsNewFile)
            {
                if (MainForm.SaveDialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                path = MainForm.SaveDialog.FileName;
            }

            _editorViewModel.SaveFileCommand.Execute(path);
            return true;
        }

        private void MakeBindings()
        {
            _path = new ObservableProperty<string>(_editorViewModel.Path, OnPathChanged);
            _text = new ObservableProperty<string>(_editorViewModel.Contents, OnContentsChanged);
            _isModified = new ObservableProperty<bool>(_editorViewModel.IsModified, OnIsModifiedChanged);

            Bindings.BindObservables(_editorViewModel.Path, _path);
            Bindings.BindObservables(_editorViewModel.Contents, _text);
            Bindings.BindObservables(_editorViewModel.IsModified, _isModified);

            RefreshTabTitle(_path.Value);
        }

        private void InitializeTextEditor()
        {
            TextEditor.Margins[0].Width = 16;
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
            RefreshTabTitle(_editorViewModel.Path);
        }

        private void OnViewTextChanged(object sender, EventArgs e)
        {
            _editorViewModel.TextChangedCommand.Execute(TextEditor.Text);
        }

        private void RefreshContents(string newValue)
        {
            if (newValue != TextEditor.Text)
            {
                TextEditor.Text = newValue;
            }
        }

        private void RefreshTabTitle(string newPath)
        {
            var newFileName = newPath is null ? "[new file]" : Path.GetFileName(_editorViewModel.Path);
            var modifiedStar = _editorViewModel.IsModified ? "*" : string.Empty;
            if (newFileName != _currentFileName)
            {
                _currentFileName = newFileName;

                SetupHighlighting(Path.GetExtension(newFileName));
            }

            TabText = _currentFileName + modifiedStar;
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
                var curLineText = TextEditor.GetTextRange(startPos,endPos - startPos);

                var indentMatch = Regex.Match(curLineText, "^[ \\t]*");
                e.Text = e.Text + indentMatch.Value;
                if (Regex.IsMatch(curLineText, "{\\s*$"))
                {
                    e.Text = e.Text + "\t";
                }
            }
        }

        private void OnTextEditorCharAdded(object sender, CharAddedEventArgs e)
        {
            //The '}' char.
            if (e.Char == 125)
            {
                var curLine = TextEditor.LineFromPosition(TextEditor.CurrentPosition);

                if (TextEditor.Lines[curLine].Text.Trim() == "}")
                {
                    //Check whether the bracket is the only thing on the line.. For cases like "if() { }".
                    SetIndent(TextEditor, curLine, GetIndent(TextEditor, curLine) - 4);
                }
            }
        }

        #region "CodeIndent Handlers"

        const int SciSetlineindentation = 2126;
        const int SciGetlineindentation = 2127;

        private void SetIndent(Scintilla scin, int line, int indent)
            => scin.DirectMessage(SciSetlineindentation, new IntPtr(line), new IntPtr(indent));

        private int GetIndent(Scintilla scin, int line)
            => scin.DirectMessage(SciGetlineindentation, new IntPtr(line), IntPtr.Zero).ToInt32();

        #endregion
    }
}