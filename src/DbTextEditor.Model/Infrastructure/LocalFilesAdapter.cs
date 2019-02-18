using DbTextEditor.Model.DAL;
using DbTextEditor.Model.Entities;
using DbTextEditor.Shared;

namespace DbTextEditor.Model.Infrastructure
{
    public class LocalFilesAdapter : IFilesAdapter
    {
        private readonly ILocalFilesRepository _repository;

        public LocalFilesAdapter(ILocalFilesRepository repository)
        {
            _repository = repository;
        }

        public void Save(FileDto model)
        {
            var entity = Map(model);
            if (!_repository.Exists(entity.Path))
            {
                _repository.Create(entity);
            }
            else
            {
                _repository.Update(entity);
            }
        }

        public FileDto Open(string fileName) =>
            _repository.Exists(fileName)
                ? Map(_repository.Get(fileName))
                : default(FileDto);

        private static LocalFileEntity Map(FileDto model) =>
            new LocalFileEntity
            {
                Path = model.FileName,
                Contents = model.Contents
            };

        private static FileDto Map(LocalFileEntity entity) =>
            new FileDto
            {
                FileName = entity.Path,
                Contents = entity.Contents
            };
    }
}