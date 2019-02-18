using System.Collections.ObjectModel;
using DbTextEditor.Model.Entities;

namespace DbTextEditor.Model
{
    public interface IDatabaseModel
    {
        ObservableCollection<DbFileEntity> Files { get; }
        void Refresh();
    }
}