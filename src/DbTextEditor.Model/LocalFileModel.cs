using DbTextEditor.Model.DAL;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Shared;

namespace DbTextEditor.Model
{
    public class LocalFileModel
    {
        public ObservableProperty<string> Path { get; } = new ObservableProperty<string>();
        public ObservableProperty<string> Contents { get; } = new ObservableProperty<string>();

        private readonly IFilesAdapter _adapter;

        public LocalFileModel(string path)
        {
            _adapter = new LocalFilesAdapter(new LocalFilesRepository());
            Path.Value = path;
            Contents.Value = _adapter.Open(Path.Value).Contents;
        }

        public void Save(FileDto model)
        {
            _adapter.Save(model);

            Path.Value = model.FileName;
            Contents.Value = model.Contents;
        }
    }
}