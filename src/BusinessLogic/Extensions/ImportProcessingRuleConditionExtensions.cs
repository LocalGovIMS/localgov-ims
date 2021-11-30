using BusinessLogic.Entities;

namespace BusinessLogic.Extensions
{
    public static class ImportProcessingRuleConditionExtensions
    {
        public static string DisplaySummary(this ImportProcessingRuleCondition condition)
        {
            if (condition.Field == null || condition.Operator == null) return string.Empty;

            var fieldAndOperator = $"{condition.Field.DisplayName} must {condition.Operator.DisplayName.ToLower()}".ToSentenceCase();

            return $"{fieldAndOperator} '{condition.Value}'";
        }
    }
}
