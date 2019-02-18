using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class RefreshDatabaseViewCommand : ICommand
    {
        // We use implementation instead of interface because Model is internal for DbTextEditor.Model assembly
        private readonly DatabaseViewViewModel _viewModel;

        public RefreshDatabaseViewCommand(DatabaseViewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute()
        {
            _viewModel.Model.Refresh();

            CommandLogger.LogExecuted<DatabaseViewViewModel, RefreshDatabaseViewCommand>();
        }
    }
}