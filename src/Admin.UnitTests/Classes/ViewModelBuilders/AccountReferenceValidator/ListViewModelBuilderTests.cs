using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.AccountReferenceValidator.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountReferenceValidator.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountReferenceValidator
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountReferenceValidatorService.Object);
        }

        private void SetupService(Mock<IAccountReferenceValidatorService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.AccountReferenceValidator.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.AccountReferenceValidator>()
                {
                    Items = new List<BusinessLogic.Entities.AccountReferenceValidator>()
                    {
                        new BusinessLogic.Entities.AccountReferenceValidator()
                        {
                            Id = 1,
                            Name = "Test"
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
                });
        }

        [TestMethod]
        public void Build_without_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void Build_with_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Build(new Models.AccountReferenceValidator.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
