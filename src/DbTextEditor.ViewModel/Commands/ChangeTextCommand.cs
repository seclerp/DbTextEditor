using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class ChangeTextCommand : ICommand<string>
    {
        private readonly IEditorViewModel _editorViewModel;

        public ChangeTextCommand(IEditorViewModel editorViewModel)
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

            CommandLogger.LogExecuted<IEditorViewModel, ChangeTextCommand>(newText);
        }
    }
}