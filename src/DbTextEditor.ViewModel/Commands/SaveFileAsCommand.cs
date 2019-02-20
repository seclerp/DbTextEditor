using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class SaveFileAsCommand : ICommand<(string Path, StorageType StorageType)>
    {
        private readonly IEditorViewModel _editorViewModel;

        public SaveFileAsCommand(IEditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute((string Path, StorageType StorageType) param)
        {
            _editorViewModel.Save(param.Path, param.StorageType);
            _editorViewModel.IsModified.Value = false;

            CommandLogger.LogExecuted<IEditorViewModel, SaveFileAsCommand>();
        }
    }
}