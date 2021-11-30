using Admin.Interfaces.ModelBuilders;
using log4net;
using Newtonsoft.Json;
using System;

namespace Admin.Classes.ViewModelBuilders
{
    public abstract class BaseViewModelBuilder<T, S> : IModelBuilder<T, S>
    {
        protected ILog Log { get; private set; }

        public BaseViewModelBuilder(ILog log)
        {
            Log = log ?? throw new ArgumentNullException("log");
        }

        public T Build()
        {
            try
            {
                Log.DebugFormat("Begin Build() of: {0}", typeof(T));
                var result = OnBuild();
                Log.Debug("End Build()");

                return result;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Error for: {0}", typeof(T)), e);
                return default;
            }
        }

        protected abstract T OnBuild();

        public T Build(S source)
        {
            try
            {
                Log.DebugFormat("Begin Build(S source) of: {0}, {1}", typeof(T), JsonConvert.SerializeObject(source));
                var result = OnBuild(source);
                Log.Debug("End Build(S source)");

                return result;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Error for: {0}, {1}", typeof(T), JsonConvert.SerializeObject(source)), e);
                return default;
            }
        }

        protected abstract T OnBuild(S source);

        public T Rebuild(T model)
        {
            try
            {
                Log.DebugFormat("Begin Rebuild(T model) of: {0}, {1}", typeof(T), JsonConvert.SerializeObject(
                    model,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
                );
                var result = OnRebuild(model);
                Log.Debug("End Rebuild(T model)");

                return result;
            }
            catch (Exception e)
            {
                Log.Error(string.Format("Error for: {0}, {1}", typeof(T), Newtonsoft.Json.JsonConvert.SerializeObject(model,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
                ), e);
                return default;
            }
        }

        protected virtual T OnRebuild(T model)
        {
            return model;
        }
    }
}