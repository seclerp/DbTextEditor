using System.Collections.ObjectModel;
using DbTextEditor.Model;
using DbTextEditor.Shared;
using DbTextEditor.ViewModel.Commands;

namespace DbTextEditor.ViewModel
{
    public class DatabaseViewViewModel : IDatabaseViewViewModel
    {
        public ObservableCollection<string> DbFileNames { get; }
        public ICommand RefreshCommand { get; }

        internal IDatabaseModel Model;

        public DatabaseViewViewModel(IDatabaseModel databaseModel)
        {
            Model = databaseModel;
            DbFileNames = new ObservableCollection<string>();
            RefreshCommand = new RefreshDatabaseViewCommand(this);

            MakeBindings();
        }

        private void MakeBindings()
        {
            Bindings.BindCollections(Model.Files, DbFileNames, entity => entity.Name);
        }
    }
}