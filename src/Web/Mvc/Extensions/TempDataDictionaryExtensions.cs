using System.Web.Mvc;

namespace Web.Mvc.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddOrUpdate(this TempDataDictionary tempData, string key, object value)
        {
            AddOrUpdate(tempData, key, value, false);
        }

        public static void AddOrUpdate(this TempDataDictionary tempData, string key, object value, bool addIfNull)
        {
            if (value is null && addIfNull == false) return;

            if (tempData.ContainsKey(key))
            {
                tempData[key] = value;
            }
            else
            {
                tempData.Add(key, value);
            }
        }
    }
}
