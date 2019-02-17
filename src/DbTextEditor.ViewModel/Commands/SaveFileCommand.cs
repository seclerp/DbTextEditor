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

        public void Execute(string path)
        {
            if (_editorViewModel.IsNewFile)
            {
                _editorViewModel.InitializeModel(path);
            }

            var model = new FileDto
            {
                FileName = path,
                Contents = _editorViewModel.Contents
            };

            _editorViewModel.Model.Save(model);
            _editorViewModel.IsModified.Value = false;

            CommandLogger.LogExecuted<MainViewModel, SaveFileCommand>();
        }
    }
}