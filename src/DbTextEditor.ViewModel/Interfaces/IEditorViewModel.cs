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
        ICommand<string> SaveFileCommand { get; }
        ObservableProperty<string> Path { get; }
        ObservableProperty<string> Contents { get; }
        ObservableProperty<bool> IsModified { get; }
        void InitializeModel(string filePath, StorageType storageType);
        void Save(FileDto file);
    }
}