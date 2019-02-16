using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Storage;
using DbTextEditor.Shared;

namespace DbTextEditor.Model.Commands
{
    public class OpenCommand : ICommand<string>
    {
        private readonly LocalFileModel _model;
        private readonly IRepository<LocalFileEntity, string> _repository;

        public OpenCommand(LocalFileModel model, IRepository<LocalFileEntity, string> repository)
        {
            _model = model;
            _repository = repository;
        }

        public void Execute(string payload)
        {
            _model.Contents = _repository.Get(payload).Contents;
            CommandLogger.LogExecuted<LocalFileModel, OpenCommand>();
        }
    }
}