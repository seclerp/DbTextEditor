using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model.Interfaces
{
  public interface IFileModel
  {
    ObservableProperty<string> Path { get; }
    ObservableProperty<string> Contents { get; }
    void Save(FileDto model);
  }
}