using DbTextEditor.Model.Commands;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Model.Storage;
using DbTextEditor.Shared;

namespace DbTextEditor.Model
{
    public class LocalFileModel : BaseNotifyPropertyChanged
    {
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (_path == value) return;
                _path = value;
                OnPropertyChanged();
            }
        }

        private string _contents;
        public string Contents
        {
            get => _contents;
            set
            {
                if (_contents == value) return;
                _contents = value;
                OnPropertyChanged();
            }
        }

        public ICommand<string> SaveCommand { get; }
        public ICommand<string> OpenCommand { get; }

        private readonly IRepository<LocalFileEntity, string> _repository;

        public LocalFileModel()
        {
            _repository = new LocalFilesRepository();
            SaveCommand = new SaveCommand(this, _repository);
            OpenCommand = new OpenCommand(this, _repository);
        }
    }
}