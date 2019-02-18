using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model
{
    public interface IFileModel
    {
        ObservableProperty<string> Path { get; }
        ObservableProperty<string> Contents { get; }
        void Save(FileDto model);
    }

    public class FileModel : IFileModel
    {
        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>();
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>();

        private readonly StorageType _storageType;
        private IFilesAdapter _adapter;
        public FileModel(string fileName, StorageType storageType)
        {
            _storageType = storageType;
            ChangeStorage(storageType);

            Path.Value = fileName;
            Contents.Value = _adapter.Open(Path.Value).Contents;
        }

        public void Save(FileDto model)
        {
            _adapter.Save(model);

            Path.Value = model.FileName;
            Contents.Value = model.Contents;
        }

        private void ChangeStorage(StorageType storageType)
        {
            if (_storageType == storageType && _adapter != null)
            {
                return;
            }

            switch (storageType)
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