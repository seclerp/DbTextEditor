namespace DbTextEditor.Shared.DataBinding.Interfaces
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<in TPayload>
    {
        void Execute(TPayload payload);
    }
}