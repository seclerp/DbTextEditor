using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
using DbTextEditor.Shared;

namespace DbTextEditor.Model.Commands
{
    public class SaveCommand : ICommand<(string Path, string Contents)>
    {
        private readonly LocalFileModel _model;
        private readonly IRepository<LocalFileEntity, string> _repository;

        public SaveCommand(LocalFileModel model, IRepository<LocalFileEntity, string> repository)
        {
            _model = model;
            _repository = repository;
        }

        public void Execute((string Path, string Contents) payload)
        {
            var entity = new LocalFileEntity
            {
                Path = payload.Path,
                Contents = payload.Contents
            };

            if (!_repository.Exists(_model.Path))
            {
                _repository.Create(entity);
            }
            else
            {
                _repository.Update(entity);
            }

            _model.Path.Value = entity.Path;
            _model.Contents.Value = entity.Contents;

            CommandLogger.LogExecuted<LocalFileModel, SaveCommand>();
        }
    }
}