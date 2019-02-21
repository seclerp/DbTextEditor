using DbTextEditor.Model.Infrastructure.Interfaces;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.Shared.Exceptions;
using DbTextEditor.Shared.Storage;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
    public class ImportCommand : ICommand<(string From, string To)>
    {
        private readonly IFilesAdapter _dbFilesAdapter;
        private readonly IFilesAdapter _localFilesAdapter;
        private readonly IMainViewModel _mainViewModel;

        public ImportCommand(IMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _localFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("LocalFilesAdapter");
            _dbFilesAdapter = CompositionRoot.Resolve<IFilesAdapter>("DbFilesAdapter");
        }

        public void Execute((string From, string To) payload)
        {
            var fromModel = _localFilesAdapter.Open(payload.From);
            if (fromModel is null)
                throw new BusinessLogicException($"File '{payload.From}' not found in local filesystem");
            _dbFilesAdapter.Save(new FileDto
            {
                FileName = payload.To,
                Contents = fromModel.Contents
            });
        }
    }
}