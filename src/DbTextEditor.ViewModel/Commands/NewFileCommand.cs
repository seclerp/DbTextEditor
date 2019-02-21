using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;
using Ninject.Parameters;

namespace DbTextEditor.ViewModel.Commands
{
    public class NewFileCommand : ICommand
    {
        private readonly IMainViewModel _mainViewModel;

        public NewFileCommand(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute()
        {
            var newEditorViewModel = CompositionRoot.Resolve<IEditorViewModel>(
                new ConstructorArgument("mainViewModel", _mainViewModel));

            _mainViewModel.OpenedEditors.Add(newEditorViewModel);

            CommandLogger.LogExecuted<IMainViewModel, NewFileCommand>();
        }
    }
}