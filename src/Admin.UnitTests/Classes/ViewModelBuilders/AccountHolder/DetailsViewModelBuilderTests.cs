using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private void SetupAccountHolderService(Mock<IAccountHolderService> service)
        {
            service.Setup(x => x.GetByAccountReference(It.IsAny<string>())).Returns(new BusinessLogic.Entities.AccountHolder()
            {
                AccountReference = "123",
                AddressLine1 = string.Empty,
                AddressLine2 = string.Empty,
                AddressLine3 = string.Empty,
                AddressLine4 = string.Empty,
                CurrentBalance = 0,
                Forename = string.Empty,
                Fund = null,
                FundCode = string.Empty,
                LastUpdated = null,
                PeriodCredit = 0,
                PeriodDebit = 0,
                Postcode = string.Empty,
                RecordType = string.Empty,
                SurnameSoundex = string.Empty,
                StopMessageReference = string.Empty,
                Surname = string.Empty,
                Title = string.Empty,
                UserField1 = string.Empty,
                UserField2 = string.Empty,
                UserField3 = string.Empty,
            });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

            SetupAccountHolderService(acountHolderService);

            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object);

            // Act
            var result = detailsViewModelBuilder.Build("123");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admin.Models.AccountHolder.DetailsViewModel));
        }

        [TestMethod]
        public void OnBuildReturnsViewModelWithAccountHolder()
        {
            // Arrange
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

            SetupAccountHolderService(acountHolderService);

            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object);

            // Act
            var result = detailsViewModelBuilder.Build("123");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(((Admin.Models.AccountHolder.DetailsViewModel)result).AccountHolder);
        }

        [TestMethod]
        public void OnBuildReturnsNull()
        {
            // Arrange
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

            SetupAccountHolderService(acountHolderService);

            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object);

            // Act
            var result = detailsViewModelBuilder.Build();

            // Assert
            Assert.IsNull(result);
        }
    }
}
