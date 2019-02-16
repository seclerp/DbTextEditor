using System.IO;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Storage;
using DbTextEditor.Shared.Exceptions;

namespace DbTextEditor.Model.Infrastructure
{
    public class LocalFilesRepository : IRepository<LocalFileEntity, string>
    {
        public bool Exists(string key)
        {
            if (key is null)
            {
                return false;
            }

            return File.Exists(key);
        }

        public void Create(LocalFileEntity entity)
        {
            File.WriteAllText(entity.Path, entity.Contents);
        }

        public LocalFileEntity Get(string key)
        {
            CheckIfFileExists(key);

            return new LocalFileEntity
            {
                Path = key,
                Contents = File.ReadAllText(key)
            };
        }

        public void Update(LocalFileEntity entity)
        {
            CheckIfFileExists(entity.Path);

            File.WriteAllText(entity.Path, entity.Contents);
        }

        public void Delete(string key)
        {
            CheckIfFileExists(key);

            File.Delete(key);
        }

        private void CheckIfFileExists(string path)
        {
            if (!File.Exists(path))
            {
                throw new BusinessLogicException($"File '{path}' doesn't exists in local filesystem");
            }
        }
    }
}