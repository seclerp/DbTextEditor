using DbTextEditor.Model;
using DbTextEditor.Shared;
using DbTextEditor.Shared.Exceptions;
using DbTextEditor.ViewModel.Commands;

namespace DbTextEditor.ViewModel
{
    public class EditorViewModel
    {
        internal LocalFileModel Model { get; private set; }
        public bool IsNewFile => Model is null;

        public ICommand<string> TextChangedCommand { get; }
        public ICommand<string> SaveFileCommand { get; }

        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>(null);
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>(string.Empty);
        public ObservableProperty<bool> IsModified { get; } = new ObservableProperty<bool>(false);

        public EditorViewModel()
        {
            TextChangedCommand = new ChangeTextCommand(this);
            SaveFileCommand = new SaveFileCommand(this);
        }

        public void InitializeModel(string filePath)
        {
            if (Model != null)
            {
                throw new BusinessLogicException("Model is already set for this view model");
            }

            Model = new LocalFileModel(filePath);
            MakeBindings();
        }

        private void MakeBindings()
        {
            Bindings.BindObservables(Model.Path, Path, BindingMode.OneWay);
            Bindings.BindObservables(Model.Contents, Contents, BindingMode.OneWay);
        }
    }
}