using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class SaveFileCommand : ICommand<string>
    {
        private readonly IEditorViewModel _editorViewModel;

        public SaveFileCommand(IEditorViewModel editorViewModel)
        {
            _editorViewModel = editorViewModel;
        }

        public void Execute(string path)
        {
            if (_editorViewModel.IsNewFile)
            {
                _editorViewModel.InitializeModel(path, StorageType.Local); // TODO
            }

            var model = new FileDto
            {
                FileName = path,
                Contents = _editorViewModel.Contents
            };

            _editorViewModel.Save(model);
            _editorViewModel.IsModified.Value = false;

            CommandLogger.LogExecuted<IEditorViewModel, SaveFileCommand>();
        }
    }
}