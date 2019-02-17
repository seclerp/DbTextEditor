using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class OpenFileCommand : ICommand<string>
    {
        private readonly MainViewModel _mainViewModel;

        public OpenFileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute(string path)
        {
            var viewModel = new EditorViewModel();
            viewModel.InitializeModel(path);
            _mainViewModel.OpenedEditors.Add(viewModel);

            CommandLogger.LogExecuted<MainViewModel, OpenFileCommand>();
        }
    }
}