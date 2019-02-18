using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class CloseEditorCommand : ICommand
    {
        private readonly IEditorViewModel _editorViewModel;

        public CloseEditorCommand(IEditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute()
        {
            _editorViewModel.MainViewModel.OpenedEditors.Remove(_editorViewModel);
        }
    }
}