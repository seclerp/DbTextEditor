using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DbTextEditor.Forms;
using DbTextEditor.ViewModel;
using ScintillaNET;

namespace DbTextEditor.Views
{
    public class EditorView
    {
        private readonly EditorForm _editorForm;
        private readonly EditorViewModel _editorViewModel;

        private string _currentFileName;

        public EditorView(EditorForm editorForm, EditorViewModel editorViewModel)
        {
            _editorForm = editorForm;
            _editorViewModel = editorViewModel;
        }

        public void OnLoad()
        {
            CreateTextEditor();
            RefreshContents();
            RefreshTabTitle();

            _editorViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _editorForm.TextEditor.TextChanged += OnViewTextChanged;
            _editorForm.TextEditor.InsertCheck += OnTextEditorInsertCheck;
            _editorForm.TextEditor.CharAdded += OnTextEditorCharAdded;
        }

        private void CreateTextEditor()
        {
            _editorForm.TextEditor = new Scintilla();

            _editorForm.Controls.Add(_editorForm.TextEditor);
            _editorForm.TextEditor.Dock = DockStyle.Fill;
            _editorForm.TextEditor.WrapMode = WrapMode.None;
            _editorForm.TextEditor.Margins[0].Width = 16;
            _editorForm.TextEditor.IndentationGuides = IndentView.LookBoth;

            _editorForm.TextEditor.SetProperty("fold", "1");
            _editorForm.TextEditor.SetProperty("fold.compact", "1");
            _editorForm.TextEditor.SetProperty("fold.html", "1");

            _editorForm.TextEditor.Margins[2].Type = MarginType.Symbol;
            _editorForm.TextEditor.Margins[2].Mask = Marker.MaskFolders;
            _editorForm.TextEditor.Margins[2].Sensitive = true;
            _editorForm.TextEditor.Margins[2].Width = 20;

            _editorForm.TextEditor.Margins[3].Type = MarginType.Color;
            _editorForm.TextEditor.Margins[3].Width = 2;
            _editorForm.TextEditor.Margins[3].BackColor = Color.DarkGray;

            _editorForm.TextEditor.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            _editorForm.TextEditor.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            _editorForm.TextEditor.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            _editorForm.TextEditor.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            _editorForm.TextEditor.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            _editorForm.TextEditor.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            _editorForm.TextEditor.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            _editorForm.TextEditor.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            _editorForm.TextEditor.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            _editorForm.TextEditor.IndentWidth = 4;
            _editorForm.TextEditor.TabWidth = 4;
            _editorForm.TextEditor.BorderStyle = BorderStyle.None;
            _editorForm.Padding = new Padding(10, 0, 0, 0);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Contents")
            {
                RefreshContents();
            }

            if (args.PropertyName == "Path" || args.PropertyName == "IsModified")
            {
                RefreshTabTitle();
            }
        }

        private void OnViewTextChanged(object sender, EventArgs e)
        {
            _editorViewModel.TextChangedCommand.Execute(_editorForm.TextEditor.Text);
        }

        private void RefreshContents()
        {
            if (_editorViewModel.Contents != _editorForm.TextEditor.Text)
            {
                _editorForm.TextEditor.Text = _editorViewModel.Contents;
            }
        }

        private void RefreshTabTitle()
        {
            var newFileName = _editorViewModel.Path is null ? "[new file]" : Path.GetFileName(_editorViewModel.Path);
            var modifiedStar = _editorViewModel.IsModified ? "*" : string.Empty;
            if (newFileName != _currentFileName)
            {
                _currentFileName = newFileName;

                SetupHighlighting(Path.GetExtension(newFileName));
            }

            _editorForm.TabText = _currentFileName + modifiedStar;
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
            _editorForm.TextEditor.Lexer = Lexer.Xml;
            _editorForm.TextEditor.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            _editorForm.TextEditor.Styles[Style.Xml.Entity].ForeColor = Color.Red;
            _editorForm.TextEditor.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            _editorForm.TextEditor.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
            _editorForm.TextEditor.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
            _editorForm.TextEditor.Styles[Style.Xml.DoubleString].ForeColor = Color.DeepPink;
            _editorForm.TextEditor.Styles[Style.Xml.SingleString].ForeColor = Color.DeepPink;
        }

        private void SetupJsonHighlighting()
        {
            SetupDefaults();
            _editorForm.TextEditor.Lexer = Lexer.Json;
            _editorForm.TextEditor.Styles[Style.Json.Default].ForeColor = Color.Silver;
            _editorForm.TextEditor.Styles[Style.Json.BlockComment].ForeColor = Color.Green;
            _editorForm.TextEditor.Styles[Style.Json.LineComment].ForeColor = Color.Green;
            _editorForm.TextEditor.Styles[Style.Json.Number].ForeColor = Color.Olive;
            _editorForm.TextEditor.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            _editorForm.TextEditor.Styles[Style.Json.String].ForeColor = Color.Red;
            _editorForm.TextEditor.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            _editorForm.TextEditor.Styles[Style.Json.Operator].ForeColor = Color.Purple;
        }

        private void SetupDefaults()
        {
            _editorForm.TextEditor.StyleResetDefault();
            _editorForm.TextEditor.Styles[Style.Default].Font = ConfigurationManager.AppSettings["Editor.Font.Family"];
            _editorForm.TextEditor.Styles[Style.Default].Size =
                Convert.ToInt32(ConfigurationManager.AppSettings["Editor.Font.Size"]);
            _editorForm.TextEditor.StyleClearAll();
        }

        private void OnTextEditorInsertCheck(object sender, InsertCheckEventArgs e)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                var startPos = _editorForm.TextEditor.Lines[_editorForm.TextEditor.LineFromPosition(_editorForm.TextEditor.CurrentPosition)].Position;
                var endPos = e.Position;
                //Text until the caret so that the whitespace is always equal in every line.
                var curLineText = _editorForm.TextEditor.GetTextRange(startPos,endPos - startPos);

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
                var curLine = _editorForm.TextEditor.LineFromPosition(_editorForm.TextEditor.CurrentPosition);

                if (_editorForm.TextEditor.Lines[curLine].Text.Trim() == "}")
                {
                    //Check whether the bracket is the only thing on the line.. For cases like "if() { }".
                    SetIndent(_editorForm.TextEditor, curLine, GetIndent(_editorForm.TextEditor, curLine) - 4);
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