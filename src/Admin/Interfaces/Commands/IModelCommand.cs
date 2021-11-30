using Admin.Classes.Commands;

namespace Admin.Interfaces.Commands
{
    public interface IModelCommand<TInput>
    {
        CommandResult Execute(TInput model);
    }
}
