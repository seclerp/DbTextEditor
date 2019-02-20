using System.Collections.ObjectModel;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Storage;

namespace DbTextEditor.ViewModel.Interfaces
{
    public interface IMainViewModel
    {
        ICommand NewFileCommand { get; }
        ICommand<(string Path, StorageType StorageType)> OpenFileCommand { get; }
        ICommand<IEditorViewModel> ChangeSelectedEditorCommand { get; }
        ObservableCollection<IEditorViewModel> OpenedEditors { get; }
        ObservableProperty<IEditorViewModel> SelectedEditor { get; }
    }
}