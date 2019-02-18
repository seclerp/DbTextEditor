using DbTextEditor.Shared.Storage;

namespace DbTextEditor.Model.Infrastructure.Interfaces
{
    public interface IFilesAdapter
    {
        void Save(FileDto model);
        FileDto Open(string fileName);
    }
}