using System.Collections.ObjectModel;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;

namespace DbTextEditor.ViewModel.Interfaces
{
    public interface IMainViewModel
    {
        ICommand NewFileCommand { get; }
        ICommand<string> OpenFileCommand { get; }
        ICommand<IEditorViewModel> ChangeSelectedEditorCommand { get; }
        ObservableCollection<IEditorViewModel> OpenedEditors { get; }
        ObservableProperty<IEditorViewModel> SelectedEditor { get; }
    }
}