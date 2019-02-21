using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Model.Interfaces;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model
{
    public class FileModel : IFileModel
    {
        private IFilesAdapter _adapter;

        private StorageType _storageType;
        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>();
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>(string.Empty);

        public ObservableProperty<StorageType> Storage { get; } =
            new ObservableProperty<StorageType>(StorageType.Local);

        public void Save(FileDto dto, StorageType storageType)
        {
            ChangeStorage(storageType);
            _adapter.Save(dto);
            Path.Value = dto.FileName;
            Contents.Value = dto.Contents;
        }

        public void Open(string fileName, StorageType storageType)
        {
            ChangeStorage(storageType);
            Path.Value = fileName;
            Contents.Value = _adapter.Open(Path.Value).Contents;
        }

        private void ChangeStorage(StorageType storageType)
        {
            Storage.Value = storageType;
            switch (Storage.Value)
            {
                case StorageType.Local:
                    _adapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
                    break;
                case StorageType.Database:
                    _adapter = CompositionRoot.Resolve<IFilesAdapter>("DbFilesAdapter");
                    break;
            }
        }
    }
}