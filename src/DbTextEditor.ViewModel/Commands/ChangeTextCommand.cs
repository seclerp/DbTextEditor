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

        public void Execute(string payload)
        {
            if (_editorViewModel.Contents != payload)
            {
                _editorViewModel.IsModified = true;
            }

            _editorViewModel.Contents = payload;

            CommandLogger.LogExecuted<EditorViewModel, ChangeTextCommand>(payload);
        }
    }
}