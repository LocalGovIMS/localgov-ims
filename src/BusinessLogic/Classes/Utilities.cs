using System;

namespace BusinessLogic.Classes
{
    public static class Utilities
    {
        public static TOutput ConvertItem<TOutput, TSource>(TSource source)
            where TSource : class
            where TOutput : class, new()
        {
            TOutput returnValue = new TOutput();

            var sourceType = typeof(TSource);
            var outputType = typeof(TOutput);

            foreach (var item in outputType.GetProperties())
            {
                var prop = sourceType.GetProperty(item.Name);

                if (!prop.GetGetMethod().IsVirtual)
                {
                    // Nullable properties
                    if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    {
                        var columnType = prop.PropertyType.GetGenericArguments()[0];

                        if (sourceType.GetProperty(item.Name).GetValue(source) != null)
                            item.SetValue(returnValue, Convert.ChangeType(sourceType.GetProperty(item.Name).GetValue(source), columnType));
                    }
                    else
                    {
                        // Normal properties.
                        item.SetValue(returnValue, Convert.ChangeType(sourceType.GetProperty(item.Name).GetValue(source), item.PropertyType));
                    }
                }
            }

            return returnValue;
        }

        public static T CopyItem<T>(T item)
            where T : class, new()
        {
            return ConvertItem<T, T>(item);
        }
    }
}
