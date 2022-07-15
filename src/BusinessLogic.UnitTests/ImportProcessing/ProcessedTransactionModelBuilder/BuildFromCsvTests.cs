using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ModelBuilder = BusinessLogic.ImportProcessing.ProcessedTransactionModelBuilder;

namespace BusinessLogic.UnitTests.ImportProcessing.ProcessedTransactionModelBuilder
{
    [TestClass]
    public class BuildFromCsvTests
    {
        private const string GoodRowData = "gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer";
        private const int ImportId = 1;

        public BuildFromCsvTests()
        {
        }

        [TestMethod]
        [DataRow(",,,,,,,,,,,,,")]
        [DataRow(",,,,,,,,,,,,,,,")]
        public void When_the_RowData_contains_an_invalid_number_of_entries_an_exception_is_thrown(string rowData)
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            Action act = () => builder.BuildFromCsvRow(rowData, ImportId);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("The row data does not contain the correct number of fields");
        }

        [TestMethod]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,202gregre1-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer", "Unable to set the Entry Date value")]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,20gregre21-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer", "Unable to set the Transaction Date value")]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,A,13,11,6.66,W0,0.2,4.44,Transfer", "Unable to set the User Code value")]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,ABC,W0,0.2,4.44,Transfer", "Unable to set the Amount value")]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,AB,4.44,Transfer", "Unable to set the VAT Rate value")]
        [DataRow("gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,ABC,Transfer", "Unable to set the VAT Amount value")]
        public void When_the_RowData_contains_an_invalid_value_the_expected_exception_is_thrown(string rowData, string expectedMessage)
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            Action act = () => builder.BuildFromCsvRow(rowData, ImportId);

            // Assert
            act.Should().Throw<InvalidCastException>().WithMessage(expectedMessage);
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_Reference_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
           var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.Reference.Should().Be("gaR4jfj2G");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_InternalReference_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.InternalReference.Should().Be("OvqPtXJ89");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_PspReference_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.PspReference.Should().Be("OvqPtXJ89");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_OfficeCode_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.OfficeCode.Should().Be("SP");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_EntryDate_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            ((DateTime)result.EntryDate).ToString("yyyy-MM-dd hh:mm:ss.fffffff").Should().Be("2021-11-22 11:30:41.8941794");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_TransactionDate_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            ((DateTime)result.TransactionDate).ToString("yyyy-MM-dd hh:mm:ss.fffffff").Should().Be("2021-11-22 11:30:41.8941794");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_AccountReference_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.AccountReference.Should().Be("12345678901IM");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_UserCode_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.UserCode.Should().Be(1);
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_FundCode_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.FundCode.Should().Be("13");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_MopCode_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.MopCode.Should().Be("11");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_Amount_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.Amount.Should().Be(6.66M);
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_VatCode_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.VatCode.Should().Be("W0");
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_VatRate_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.VatRate.Should().Be(0.2F);
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_VatAmount_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.VatAmount.Should().Be(4.44M);
        }

        [TestMethod]
        public void When_valid_RowData_is_provided_the_Narrative_field_is_set_correctly()
        {
            // Arrange
            var builder = new ModelBuilder();

            // Act
            var result = builder.BuildFromCsvRow(GoodRowData, ImportId);

            // Assert
            result.Narrative.Should().Be("Transfer");
        }
    }
}
