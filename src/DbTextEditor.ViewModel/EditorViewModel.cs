using DbTextEditor.Model.Interfaces;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class EditorViewModel : IEditorViewModel
    {
        public EditorViewModel(IMainViewModel mainViewModel)
        {
            Model = CreateModel();
            MainViewModel = mainViewModel;
            TextChangedCommand = new ChangeTextCommand(this);
            SaveFileCommand = new SaveFileCommand(this);
            SaveFileAsCommand = new SaveFileAsCommand(this);
        }

        internal IFileModel Model { get; }
        public IMainViewModel MainViewModel { get; }
        public bool IsNewFile => Path.Value is null;

        public ICommand<string> TextChangedCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand<(string Path, StorageType StorageType)> SaveFileAsCommand { get; }

        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>(null);
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>(string.Empty);
        public ObservableProperty<bool> IsModified { get; } = new ObservableProperty<bool>(false);

        public ObservableProperty<StorageType> Storage { get; } =
            new ObservableProperty<StorageType>(StorageType.Local);

        public void Open(string fileName, StorageType storageType)
        {
            Model.Open(fileName, storageType);
        }

        public void Save(string newFileName, StorageType storageType)
        {
            Model.Save(new FileDto
            {
                FileName = newFileName,
                Contents = Contents
            }, storageType);
        }

        private IFileModel CreateModel()
        {
            var model = CompositionRoot.Resolve<IFileModel>();
            MakeBindings(model);
            return model;
        }

        private void MakeBindings(IFileModel model)
        {
            Bindings.BindObservables(model.Path, Path, BindingMode.OneWay);
            Bindings.BindObservables(model.Contents, Contents, BindingMode.OneWay);
            Bindings.BindObservables(model.Storage, Storage, BindingMode.OneWay);
        }
    }
}