using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;
using Ninject.Parameters;

namespace DbTextEditor.ViewModel.Commands
{
    public class OpenFileCommand : ICommand<(string Path, StorageType StorageType)>
    {
        private readonly IMainViewModel _mainViewModel;

        public OpenFileCommand(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute((string Path, StorageType StorageType) param)
        {
            var editorViewModel = CompositionRoot.Resolve<IEditorViewModel>(
                new ConstructorArgument("mainViewModel", _mainViewModel));

            editorViewModel.Open(param.Path, param.StorageType);
            _mainViewModel.OpenedEditors.Add(editorViewModel);

            CommandLogger.LogExecuted<IMainViewModel, OpenFileCommand>();
        }
    }
}