using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class SaveFileCommand : ICommand
    {
        private readonly IEditorViewModel _editorViewModel;

        public SaveFileCommand(IEditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute()
        {
            _editorViewModel.Save(_editorViewModel.Path, _editorViewModel.Storage);
            _editorViewModel.IsModified.Value = false;

            CommandLogger.LogExecuted<IEditorViewModel, SaveFileCommand>();
        }
    }
}