using System;
using System.Windows.Input;
using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class ChangeTextCommand : ICommand<string>
    {
        private readonly EditorViewModel _editorViewModel;

        public ChangeTextCommand(EditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute(string newText)
        {
            if (_editorViewModel.Contents != newText)
            {
                _editorViewModel.IsModified.Value = true;
            }

            _editorViewModel.Contents.Value = newText;

            CommandLogger.LogExecuted<EditorViewModel, ChangeTextCommand>(newText);
        }
    }
}