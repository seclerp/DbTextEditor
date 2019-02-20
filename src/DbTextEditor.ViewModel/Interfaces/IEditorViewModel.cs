using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.ViewModel.Interfaces
{
    public interface IEditorViewModel
    {
        IMainViewModel MainViewModel { get; }
        bool IsNewFile { get; }
        ICommand<string> TextChangedCommand { get; }
        ICommand SaveFileCommand { get; }
        ICommand<(string Path, StorageType StorageType)> SaveFileAsCommand { get; }
        ObservableProperty<string> Path { get; }
        ObservableProperty<string> Contents { get; }
        ObservableProperty<bool> IsModified { get; }
        ObservableProperty<StorageType> Storage { get; }
        void Open(string fileName, StorageType storageType);
        void Save(string newFileName, StorageType storageType);
    }
}