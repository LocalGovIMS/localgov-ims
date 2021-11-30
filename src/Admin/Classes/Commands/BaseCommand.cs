using Admin.Interfaces.Commands;
using log4net;
using System;

namespace Admin.Classes.Commands
{
    public abstract class BaseCommand<T> : IModelCommand<T>
    {
        protected ILog Log { get; private set; }

        public BaseCommand(ILog log)
        {
            Log = log ?? throw new ArgumentNullException("log");
        }

        public CommandResult Execute(T model)
        {
            try
            {
                Log.DebugFormat("Begin Execute(T model): {0}", Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var result = OnExecute(model);
                Log.Debug("End Execute(T model)");

                return result;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Error for: {0}, {1}", typeof(T), Newtonsoft.Json.JsonConvert.SerializeObject(model)), e);
                return new CommandResult(false, "An error occurred processing the command");
            }
        }

        protected abstract CommandResult OnExecute(T model);
    }
}