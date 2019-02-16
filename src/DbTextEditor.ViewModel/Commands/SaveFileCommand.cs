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

        public void Execute(string payload)
        {
            if (_editorViewModel.IsNewFile)
            {
                _editorViewModel.InitializeModel(payload);
            }

            _editorViewModel.Model.SaveCommand.Execute(payload);
            _editorViewModel.IsModified = false;

            CommandLogger.LogExecuted<MainViewModel, SaveFileCommand>();
        }
    }
}