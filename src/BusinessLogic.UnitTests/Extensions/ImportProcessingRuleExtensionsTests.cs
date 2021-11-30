using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions
{
    [TestClass]
    public class ImportProcessingRuleExtensionsTests
    {
        [TestMethod]
        public void Groups_returns_a_distinct_list_of_all_groups()
        {
            // Arrange
            var rule = GetImportProcessingRule();

            // Act
            var groups = rule.Groups();

            // Assert
            groups.Count().Should().Be(2);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        public void GroupConditions_returns_the_expected_number_of_conditions(int group, int expectedNumberOfConditions)
        {
            // Arrange
            var rule = GetImportProcessingRule();

            // Act
            var conditions = rule.GroupConditions(group);

            // Assert
            conditions.Count().Should().Be(expectedNumberOfConditions);
        }

        private ImportProcessingRule GetImportProcessingRule()
        {
            return new ImportProcessingRule()
            {
                Conditions = new List<Entities.ImportProcessingRuleCondition>()
                {
                    new Entities.ImportProcessingRuleCondition
                    {
                        Id = 1,
                        Group = 1
                    },
                    new Entities.ImportProcessingRuleCondition
                    {
                        Id = 2,
                        Group = 2
                    },
                    new Entities.ImportProcessingRuleCondition
                    {
                        Id = 3,
                        Group = 2
                    }
                }
            };
        }
    }
}
