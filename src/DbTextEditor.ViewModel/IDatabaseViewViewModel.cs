using System.Collections.ObjectModel;
using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel
{
    public interface IDatabaseViewViewModel
    {
        ObservableCollection<string> DbFileNames { get; }
        ICommand RefreshCommand { get; }
    }
}