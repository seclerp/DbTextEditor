using System;
using DbTextEditor.Shared;

namespace DbTextEditor.ViewModel.Commands
{
    public class NewFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        public NewFileCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Execute()
        {
            _mainViewModel.OpenedEditors.Add(new EditorViewModel
            {
                Path = null,
                Contents = string.Empty
            });

            CommandLogger.LogExecuted<MainViewModel, NewFileCommand>();
        }
    }
}