using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;
using Ninject.Parameters;

namespace DbTextEditor.ViewModel.Commands
{
    public class OpenFileCommand : ICommand<string>
    {
        private readonly IMainViewModel _mainViewModel;

        public OpenFileCommand(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute(string path)
        {
            var editorViewModel = CompositionRoot.Resolve<IEditorViewModel>(
                new ConstructorArgument("mainViewModel", _mainViewModel));

            editorViewModel.InitializeModel(path, StorageType.Local); // TODO
            _mainViewModel.OpenedEditors.Add(editorViewModel);

            CommandLogger.LogExecuted<IMainViewModel, OpenFileCommand>();
        }
    }
}