using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Storage;
using DbTextEditor.Shared;

namespace DbTextEditor.Model.Commands
{
    public class SaveCommand : ICommand<string>
    {
        private readonly LocalFileModel _model;
        private readonly IRepository<LocalFileEntity, string> _repository;

        public SaveCommand(LocalFileModel model, IRepository<LocalFileEntity, string> repository)
        {
            _model = model;
            _repository = repository;
        }

        public void Execute(string payload)
        {
            var entity = new LocalFileEntity
            {
                Path = _model.Path,
                Contents = payload
            };

            if (!_repository.Exists(_model.Path))
            {
                _repository.Create(entity);
            }
            else
            {
                _repository.Update(entity);
            }
            CommandLogger.LogExecuted<LocalFileModel, SaveCommand>();
        }
    }
}