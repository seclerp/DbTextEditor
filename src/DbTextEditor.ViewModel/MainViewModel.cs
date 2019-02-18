using System.Collections.ObjectModel;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public ICommand NewFileCommand { get; }
        public ICommand<string> OpenFileCommand { get; }
        public ObservableCollection<IEditorViewModel> OpenedEditors { get; } = new ObservableCollection<IEditorViewModel>();

        public MainViewModel()
        {
            NewFileCommand = new NewFileCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
        }
    }
}