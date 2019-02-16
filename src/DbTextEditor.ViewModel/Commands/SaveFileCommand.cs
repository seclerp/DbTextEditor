using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class SaveFileCommand : ICommand<string>
    {
        private readonly EditorViewModel _editorViewModel;

        public SaveFileCommand(EditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute(string path)
        {
            if (_editorViewModel.IsNewFile)
            {
                _editorViewModel.InitializeModel(path);
            }

            _editorViewModel.Model.SaveCommand.Execute((path, _editorViewModel.Contents));
            _editorViewModel.IsModified = false;

            CommandLogger.LogExecuted<MainViewModel, SaveFileCommand>();
        }
    }
}