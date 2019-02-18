using System.Collections.ObjectModel;
using DbTextEditor.Shared.DataBinding.Interfaces;

namespace DbTextEditor.ViewModel.Interfaces
{
    public interface IDatabaseViewViewModel
    {
        ObservableCollection<string> DbFileNames { get; }
        ICommand RefreshCommand { get; }
        void Refresh();
    }
}