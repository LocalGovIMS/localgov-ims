using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.AccountHolder.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountHolder.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountHolder
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IFundMessageService> _mockFundMessageService = new Mock<IFundMessageService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountHolderService.Object,
               _mockFundService.Object,
               _mockFundMessageService.Object);
        }

        private void SetupAccountHolderService(Mock<IAccountHolderService> service)
        {
            service.Setup(x => x.GetByAccountReference(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.AccountHolder()
                {
                    AccountReference = "AccountReference"
                });
        }

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetAllFunds(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Fund> {
                    new BusinessLogic.Entities.Fund()
                    {
                        FundCode = "F1",
                        FundName = "Fund Name"
                    }
                });
        }

        private void SetupFundMessageService(Mock<IFundMessageService> service)
        {
            service.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.FundMessage>()
                {
                    {
                        new BusinessLogic.Entities.FundMessage()
                        {
                            Message = "A message"
                        }
                    }
                });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildReturnsViewModelWithFunds()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.Funds.Should().NotBeNull();
        }

        [TestMethod]
        public void OnBuildReturnsViewModelWithFundMessages()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.FundMessages.Should().NotBeNull();
        }

        [TestMethod]
        public void OnBuildWithAnAccountHolderReferenceThatDoesNotMatchAnExistingAccountHolderReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModel()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithFunds()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.Funds.Should().NotBeNull();
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithFundMessages()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.FundMessages.Should().NotBeNull();
        }
    }
}
