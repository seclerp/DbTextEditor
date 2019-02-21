using System.Collections.ObjectModel;
using DbTextEditor.Model.Interfaces;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class DatabaseViewViewModel : IDatabaseViewViewModel
    {
        private readonly IDatabaseModel _model;

        public DatabaseViewViewModel(IDatabaseModel databaseModel)
        {
            _model = databaseModel;
            DbFileNames = new ObservableCollection<string>();
            RefreshCommand = new RefreshDatabaseViewCommand(this);

            MakeBindings();
        }

        public ObservableCollection<string> DbFileNames { get; }
        public ICommand RefreshCommand { get; }

        public void Refresh()
        {
            _model.Refresh();
        }

        private void MakeBindings()
        {
            Bindings.BindCollections(_model.Files, DbFileNames, entity => entity.Name);
        }
    }
}