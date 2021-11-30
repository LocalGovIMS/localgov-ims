using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.ImportProcessing.RuleEngine
{
    public static class TestHelpers
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithSingleConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Contains value", FundCode = "99" },
                        "01",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithSingleConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "A non matching value", FundCode = "99" },
                        "99",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Contains value", FundCode = "99" },
                        "02",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "A non matching value", FundCode = "98" },
                        "98",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { FundCode = "99" },
                        "03",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { FundCode = "98" },
                        "03",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetSingleGroupWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { FundCode = "97" },
                        "97",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithSingleConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Contains value", FundCode = "99" },
                        "04",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithSingleConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "A non matching value", FundCode = "98" },
                        "04",
                    }
                    ,
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithSingleConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "A non matching value", FundCode = "97" },
                        "97",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { Narrative = "Narrative value", FundCode = "99" },
                        "05",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference value", FundCode = "98" },
                        "05",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { Narrative = "Narrative non mathcing value", FundCode = "99" },
                        "99",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference non matching value", FundCode = "98" },
                        "98",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { Narrative = "Narrative value", FundCode = "97" },
                        "97",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleAndConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference value", FundCode = "97" },
                        "97",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { Narrative = "Narrative value", FundCode = "50" },
                        "06",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { Narrative = "Narrative non matching value", FundCode = "99" },
                        "06",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference value", FundCode = "50" },
                        "06",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference non matching value", FundCode = "98" },
                        "06",
                    },
                    new object[]
                    {
                        new List<ImportProcessingRule>()
                        {
                            GetMultipleGroupsWithMultipleOrConditionRule()
                        },
                        new ProcessedTransaction() { AccountReference = "Account Reference non matching value", FundCode = "50" },
                        "50",
                    },
                };
            }
        }

        private static ImportProcessingRule GetSingleGroupWithSingleConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, AccountReferenceField, ContainOperator, "Contains value")
                .AddAction(FundCodeField, "01");
        }

        private static ImportProcessingRule GetSingleGroupWithMultipleAndConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, AccountReferenceField, ContainOperator, "Contains value")
                .AddCondition(LogicalOperationType.And, 1, FundCodeField, EqualOperator, "99")
                .AddAction(FundCodeField, "02");
        }

        private static ImportProcessingRule GetSingleGroupWithMultipleOrConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, FundCodeField, EqualOperator, "99")
                .AddCondition(LogicalOperationType.Or, 1, FundCodeField, EqualOperator, "98")
                .AddAction(FundCodeField, "03");
        }

        private static ImportProcessingRule GetMultipleGroupsWithSingleConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, FundCodeField, EqualOperator, "99")
                .AddCondition(null, 2, FundCodeField, EqualOperator, "98")
                .AddAction(FundCodeField, "04");
        }

        private static ImportProcessingRule GetMultipleGroupsWithMultipleAndConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, FundCodeField, EqualOperator, "99")
                .AddCondition(LogicalOperationType.And, 1, NarrativeField, EqualOperator, "Narrative value")
                .AddCondition(null, 2, FundCodeField, EqualOperator, "98")
                .AddCondition(LogicalOperationType.And, 2, AccountReferenceField, EqualOperator, "Account Reference value")
                .AddAction(FundCodeField, "05");
        }

        private static ImportProcessingRule GetMultipleGroupsWithMultipleOrConditionRule()
        {
            return new ImportProcessingRule()
                .AddCondition(null, 1, FundCodeField, EqualOperator, "99")
                .AddCondition(LogicalOperationType.Or, 1, NarrativeField, EqualOperator, "Narrative value")
                .AddCondition(null, 2, FundCodeField, EqualOperator, "98")
                .AddCondition(LogicalOperationType.Or, 2, AccountReferenceField, EqualOperator, "Account Reference value")
                .AddAction(FundCodeField, "06");
        }

        public static ImportProcessingRule AddCondition(
            this ImportProcessingRule rule,
            string logicalOperator,
            int group,
            ImportProcessingRuleField field,
            ImportProcessingRuleOperator @operator,
            string value)
        {
            rule.Conditions.Add(new ImportProcessingRuleCondition()
            {
                LogicalOperator = logicalOperator,
                Group = group,
                ImportProcessingRuleFieldId = field.Id,
                Field = field,
                ImportProcessingRuleOperatorId = @operator.Id,
                Operator = @operator,
                Value = value
            });

            return rule;
        }

        public static ImportProcessingRule AddAction(
            this ImportProcessingRule rule,
            ImportProcessingRuleField field,
            string value)
        {
            rule.Actions.Add(new ImportProcessingRuleAction()
            {
                ImportProcessingRuleFieldId = field.Id,
                Field = field,
                Value = value
            });

            return rule;
        }

        private static List<ImportProcessingRuleField> GetFields()
        {
            return new List<ImportProcessingRuleField>()
            {
                new ImportProcessingRuleField() { Id = 1, Name = "AccountReference", Type = FieldType.Text },
                new ImportProcessingRuleField() { Id = 2, Name = "FundCode", Type = FieldType.Text },
                new ImportProcessingRuleField() { Id = 3, Name = "Narrative", Type = FieldType.Text }
            };
        }

        private static ImportProcessingRuleField AccountReferenceField => GetFields().FirstOrDefault(x => x.Id == 1);
        private static ImportProcessingRuleField FundCodeField => GetFields().FirstOrDefault(x => x.Id == 2);
        private static ImportProcessingRuleField NarrativeField => GetFields().FirstOrDefault(x => x.Id == 3);

        private static List<ImportProcessingRuleOperator> GetOperators()
        {
            return new List<ImportProcessingRuleOperator>()
            {
                new ImportProcessingRuleOperator() { Id = 1, Name = "Contain" },
                new ImportProcessingRuleOperator() { Id = 2, Name = "NotContain" },
                new ImportProcessingRuleOperator() { Id = 3, Name = "StartWith" },
                new ImportProcessingRuleOperator() { Id = 4, Name = "EndWith" },
                new ImportProcessingRuleOperator() { Id = 5, Name = "BeGreaterThan" },
                new ImportProcessingRuleOperator() { Id = 6, Name = "BeLessThan" },
                new ImportProcessingRuleOperator() { Id = 7, Name = "Equal" }
            };
        }

        private static ImportProcessingRuleOperator ContainOperator => GetOperators().FirstOrDefault(x => x.Id == 1);
        private static ImportProcessingRuleOperator NotContainOperator => GetOperators().FirstOrDefault(x => x.Id == 2);
        private static ImportProcessingRuleOperator StartWithOperator => GetOperators().FirstOrDefault(x => x.Id == 3);
        private static ImportProcessingRuleOperator EndWithOperator => GetOperators().FirstOrDefault(x => x.Id == 4);
        private static ImportProcessingRuleOperator BeGreaterThanOperator => GetOperators().FirstOrDefault(x => x.Id == 5);
        private static ImportProcessingRuleOperator BeLessThanOperator => GetOperators().FirstOrDefault(x => x.Id == 6);
        private static ImportProcessingRuleOperator EqualOperator => GetOperators().FirstOrDefault(x => x.Id == 7);
    }
}

