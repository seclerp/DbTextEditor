using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DbTextEditor.Shared
{
    public class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ExecuteCommand(ICommand command, object payload)
        {
            command.Execute();
        }

        public void ExecuteCommand<TPayload>(ICommand<TPayload> command, TPayload payload)
        {
            command.Execute(payload);
        }
    }
}