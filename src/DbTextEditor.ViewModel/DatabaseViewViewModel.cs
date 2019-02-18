using System.Collections.ObjectModel;
using DbTextEditor.Model;
using DbTextEditor.Model.Interfaces;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class DatabaseViewViewModel : IDatabaseViewViewModel
    {
        public ObservableCollection<string> DbFileNames { get; }
        public ICommand RefreshCommand { get; }

        private IDatabaseModel _model;

        public DatabaseViewViewModel(IDatabaseModel databaseModel)
        {
            _model = databaseModel;
            DbFileNames = new ObservableCollection<string>();
            RefreshCommand = new RefreshDatabaseViewCommand(this);

            MakeBindings();
        }

        private void MakeBindings()
        {
            Bindings.BindCollections(_model.Files, DbFileNames, entity => entity.Name);
        }

        public void Refresh()
        {
            _model.Refresh();
        }
    }
}