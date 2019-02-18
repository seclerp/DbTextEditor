using System.Collections.ObjectModel;
using DbTextEditor.Model.DAL;
using DbTextEditor.Model.DAL.Interfaces;
using DbTextEditor.Model.Entities;
using DbTextEditor.Model.Interfaces;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;

namespace DbTextEditor.Model
{
    public class DatabaseModel : IDatabaseModel
    {
        private readonly IDbFilesRepository _adapter;
        public ObservableCollection<DbFileEntity> Files { get; } = new ObservableCollection<DbFileEntity>();

        public DatabaseModel(IDbFilesRepository adapter)
        {
            _adapter = adapter;
        }

        public void Refresh()
        {
            ObservableCollectionHelper.ClearObservableCollection(Files);
            foreach (var file in _adapter.GetAll())
            {
                Files.Add(file);
            }
        }
    }
}