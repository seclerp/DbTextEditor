using System.Collections.ObjectModel;
using DbTextEditor.Shared;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Commands;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel
{
    public class MainViewModel : IMainViewModel
    {
        public ICommand NewFileCommand { get; }
        public ICommand<(string Path, StorageType StorageType)> OpenFileCommand { get; }
        public ICommand<(string From, string To)> ExportCommand{ get; }
        public ICommand<(string From, string To)> ImportCommand{ get; }
        public ObservableCollection<IEditorViewModel> OpenedEditors { get; } = new ObservableCollection<IEditorViewModel>();
        public ObservableProperty<IEditorViewModel> SelectedEditor { get; } = new ObservableProperty<IEditorViewModel>();

        public MainViewModel()
        {
            NewFileCommand = new NewFileCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
            ExportCommand = new ExportCommand(this);
            ImportCommand = new ImportCommand(this);
        }
    }
}