using System.Windows.Input;
using DbTextEditor.Shared.DataBinding.Interfaces;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.ViewModel.Commands
{
  public class ChangeSelectedEditorCommand : ICommand<IEditorViewModel>
  {
    private readonly IMainViewModel _viewModel;

    public ChangeSelectedEditorCommand(IMainViewModel viewModel)
    {
      _viewModel = viewModel;
    }

    public void Execute(IEditorViewModel viewModel)
    {
      _viewModel.SelectedEditor.Value = viewModel;
    }
  }
}