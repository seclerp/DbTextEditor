using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model.Interfaces
{
  public interface IFileModel
  {
    ObservableProperty<string> Path { get; }
    ObservableProperty<string> Contents { get; }
    ObservableProperty<StorageType> Storage { get; }
    void Save(FileDto dto, StorageType storageType);
    void Open(string fileName, StorageType storageType);
  }
}