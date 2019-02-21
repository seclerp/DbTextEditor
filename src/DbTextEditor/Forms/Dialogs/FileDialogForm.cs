using System.Windows.Forms;
using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms.Dialogs
{
    public class FileDialogForm : Form
    {
        protected IFilesAdapter DbFilesAdapter;
        protected IDatabaseViewViewModel DbViewModel;

        protected IFilesAdapter LocalFilesAdapter;

        public FileDialogForm()
        {
            LocalFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
            DbFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("DbFilesAdapter");
            DbViewModel = CompositionRoot.Resolve<IDatabaseViewViewModel>();
        }

        public string FileName { get; protected set; } = "";
        public StorageType StorageType { get; protected set; }
    }
}