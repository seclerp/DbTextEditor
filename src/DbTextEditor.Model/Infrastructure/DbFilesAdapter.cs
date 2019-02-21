using System.Text;
using DbTextEditor.Model.DAL.Interfaces;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model.Infrastructure
{
    public class DbFilesAdapter : IFilesAdapter
    {
        private readonly IDbFilesRepository _repository;

        public DbFilesAdapter(IDbFilesRepository repository)
        {
            _repository = repository;
        }

        public void Save(FileDto model)
        {
            var entity = Map(model);
            if (!_repository.Exists(entity.Name))
                _repository.Create(entity);
            else
                _repository.Update(entity);
        }

        public FileDto Open(string fileName)
        {
            return _repository.Exists(fileName)
                ? Map(_repository.Get(fileName))
                : default(FileDto);
        }

        public bool Exists(string fileName)
        {
            return _repository.Exists(fileName);
        }

        private static DbFileEntity Map(FileDto model)
        {
            return new DbFileEntity
            {
                Name = model.FileName,
                Contents = Encoding.UTF8.GetBytes(model.Contents)
            };
        }

        private static FileDto Map(DbFileEntity entity)
        {
            return new FileDto
            {
                FileName = entity.Name,
                Contents = Encoding.UTF8.GetString(entity.Contents)
            };
        }
    }
}