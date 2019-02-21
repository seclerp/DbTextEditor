using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class RefreshDatabaseViewCommand : ICommand
    {
        // We use implementation instead of interface because Model is internal for DbTextEditor.Model assembly
        private readonly IDatabaseViewViewModel _viewModel;

        public RefreshDatabaseViewCommand(IDatabaseViewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute()
        {
            _viewModel.Refresh();

            CommandLogger.LogExecuted<IDatabaseViewViewModel, RefreshDatabaseViewCommand>();
        }
    }
}