using System.Windows.Forms;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms.Dialogs
{
    public class FileDialogForm : Form
    {
        public string FileName { get; protected set; } = "";
        public StorageType StorageType { get; protected set; }

        protected IFilesAdapter LocalFilesAdapter;
        protected IFilesAdapter DbFilesAdapter;
        protected IDatabaseViewViewModel DbViewModel;

        public FileDialogForm()
        {
            LocalFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
            DbFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("DbFilesAdapter");
            DbViewModel = CompositionRoot.Resolve<IDatabaseViewViewModel>();
        }
    }
}
