using DbTextEditor.Shared;

namespace DbTextEditor.Model.Infrastructure
{
    public interface IFilesAdapter
    {
        void Save(FileDto model);
        FileDto Open(string fileName);
    }
}