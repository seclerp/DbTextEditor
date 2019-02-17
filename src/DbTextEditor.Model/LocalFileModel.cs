using DbTextEditor.Model.Commands;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Shared;

namespace DbTextEditor.Model
{
    public class LocalFileModel : BaseNotifyPropertyChanged
    {
        public ObservableProperty<string> Path { get; set; } = new ObservableProperty<string>();
        public ObservableProperty<string> Contents { get; set; } = new ObservableProperty<string>();

        public ICommand<(string Path, string Contents)> SaveCommand { get; }

        private readonly IRepository<LocalFileEntity, string> _repository;

        public LocalFileModel(string path)
        {
            _repository = new LocalFilesRepository();
            SaveCommand = new SaveCommand(this, _repository);
            Path.Value = path;
            Contents.Value = _repository.Get(Path.Value).Contents;
        }
    }
}