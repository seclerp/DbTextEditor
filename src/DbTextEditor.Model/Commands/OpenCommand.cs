using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure;
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

        public void Execute(string path)
        {
            _model.Contents = _repository.Get(path).Contents;
            CommandLogger.LogExecuted<LocalFileModel, OpenCommand>();
        }
    }
}