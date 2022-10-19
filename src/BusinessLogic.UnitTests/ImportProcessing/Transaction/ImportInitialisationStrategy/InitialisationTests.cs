using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Services;
using FluentAssertions;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportInitialisationStrategy
{
    [TestClass]
    public class InitialiseTests : TestBase
    {
        public const int MetadataKeyId = 1;

        public InitialiseTests()
        {
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(CompositeResolver.Create(new IFormatterResolver[]
            {
                // This can solve DateTime time zone problem
                NativeDateTimeResolver.Instance,
                ContractlessStandardResolver.Instance
            }));

            }

        [TestMethod]
        public void Initialise_CallsMetadataKeyServiceOnce()
        {
            // Arrange
            SetupDependencies(MetadataKeyId);
            SetupStrategy();

            // Act
            Strategy.Initialise(GetArgs(Transaction()));

            // Assert
            MockMetadataKeyService.Verify(x => x.Search(It.IsAny<BusinessLogic.Models.MetadataKey.SearchCriteria>()), Times.Once);
        }

        [TestMethod]
        public void Process_CreatesImportMetadataWithTheCorrectKey()
        {
            // Arrange
            SetupDependencies(MetadataKeyId);
            SetupStrategy();

            var args = GetArgs(Transaction());

            // Act
            Strategy.Initialise(args);

            // Assert
            args.Import.Metadata.Should().NotBeNull();
            args.Import.Metadata.Should().NotBeEmpty();
            args.Import.Metadata.Count.Should().Be(1);
            args.Import.Metadata.First().MetadataKeyId.Should().Be(MetadataKeyId);
        }

        [TestMethod]
        public void Process_CreatesImportMetadataWithTheCorrectValue()
        {
            // Arrange
            SetupDependencies(MetadataKeyId);
            SetupStrategy();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            var args = GetArgs(Transaction());
            var totalAmount = args.ImportRows.Sum(x => x.ToProcessedTransaction().Amount);
            var expectedValue = string.Format("{0:C}", totalAmount).ToString();

            // Act
            Strategy.Initialise(args);

            // Assert
            args.Import.Metadata.Should().NotBeNull();
            args.Import.Metadata.Should().NotBeEmpty();
            args.Import.Metadata.Count.Should().Be(1);
            args.Import.Metadata.First().Value.Should().Be(expectedValue);
        }
    }
}
