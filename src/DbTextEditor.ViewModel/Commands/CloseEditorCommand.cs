using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class CloseEditorCommand : ICommand
    {
        private readonly EditorViewModel _editorViewModel;

        public CloseEditorCommand(EditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute()
        {
            _editorViewModel.MainViewModel.OpenedEditors.Remove(_editorViewModel);
        }
    }
}