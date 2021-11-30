using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class ObjectExtensions
    {
        public static dynamic GetPropertyValue(this object t, string propertyName)
        {
            object val = t.GetType().GetProperties().Single(pi => pi.Name == propertyName).GetValue(t, null);
            return val;
        }

        public static void SetPropertyValue<TResult>(this object t, string propertyName, string value)
        {
            t.GetType().GetProperty(propertyName).SetValue(t, value, null);
        }

        public static T TrimStringProperties<T>(this T input)
        {
            var stringProperties = input
                .GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(input, null);

                if (currentValue != null)
                    stringProperty.SetValue(input, currentValue.Trim(), null);
            }

            return input;
        }
    }
}
