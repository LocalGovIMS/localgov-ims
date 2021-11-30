using BusinessLogic.Entities;

namespace BusinessLogic.Extensions
{
    public static class ImportProcessingRuleActionExtensions
    {
        public static string DisplaySummary(this ImportProcessingRuleAction action)
        {
            if (action.Field == null) return string.Empty;

            var field = $"{action.Field.DisplayName} to".ToSentenceCase();

            return $"{field} '{action.Value}'";
        }
    }
}
