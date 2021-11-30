namespace BusinessLogic.Entities
{
    public partial class ImportProcessingRuleAction
    {
        public ImportProcessingRuleAction()
        {
        }

        public int Id { get; set; }

        public int ImportProcessingRuleId { get; set; }

        public int ImportProcessingRuleFieldId { get; set; }

        public string Value { get; set; }

        public virtual ImportProcessingRule Rule { get; set; }

        public virtual ImportProcessingRuleField Field { get; set; }
    }
}
