using BusinessLogic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class ImportProcessingRuleExtensions
    {
        public static IEnumerable<int> Groups(this ImportProcessingRule rule)
        {
            return rule.Conditions.Select(x => x.Group).Distinct();
        }

        public static IOrderedEnumerable<ImportProcessingRuleCondition> GroupConditions(this ImportProcessingRule rule, int group)
        {
            return rule.Conditions.Where(x => x.Group == group).OrderBy(x => x.Id);
        }
    }
}
