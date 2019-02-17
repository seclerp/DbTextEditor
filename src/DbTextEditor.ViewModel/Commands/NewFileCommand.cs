using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class NewFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public NewFileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute()
        {
            var newEditorViewModel = new EditorViewModel(_mainViewModel);
            _mainViewModel.OpenedEditors.Add(newEditorViewModel);

            CommandLogger.LogExecuted<MainViewModel, NewFileCommand>();
        }
    }
}