using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class CheckDigitConfigurationExtensions
    {
        public static int[] WeightingsArray(this CheckDigitConfiguration source)
        {
            if (string.IsNullOrWhiteSpace(source.Weightings))
                return new int[0];

            return source.Weightings.Split(',')
                .Select(x => int.Parse(x))
                .ToArray();
        }

        public static Dictionary<string, string> ResultSubstitutionsDictionary(this CheckDigitConfiguration source)
        {
            if (string.IsNullOrWhiteSpace(source.ResultSubstitutions))
                return new Dictionary<string, string>();

            return source.ResultSubstitutions.Split(',')
                .ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);
        }
    }
}
