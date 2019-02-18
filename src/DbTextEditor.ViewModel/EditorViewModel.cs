using DbTextEditor.Model;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Exceptions;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class EditorViewModel : IEditorViewModel
    {
        public IMainViewModel MainViewModel { get; }
        internal IFileModel Model { get; private set; }
        public bool IsNewFile => Model is null;

        public ICommand<string> TextChangedCommand { get; }
        public ICommand<string> SaveFileCommand { get; }

        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>(null);
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>(string.Empty);
        public ObservableProperty<bool> IsModified { get; } = new ObservableProperty<bool>(false);

        public EditorViewModel(IMainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            TextChangedCommand = new ChangeTextCommand(this);
            SaveFileCommand = new SaveFileCommand(this);
        }

        public void InitializeModel(string filePath, StorageType storageType)
        {
            if (Model != null)
            {
                throw new BusinessLogicException("Model is already set for this view model");
            }

            Model = new FileModel(filePath, storageType);
            MakeBindings();
        }

        private void MakeBindings()
        {
            Bindings.BindObservables(Model.Path, Path, BindingMode.OneWay);
            Bindings.BindObservables(Model.Contents, Contents, BindingMode.OneWay);
        }

        public void Save(FileDto file)
        {
            Model.Save(file);
        }
    }
}