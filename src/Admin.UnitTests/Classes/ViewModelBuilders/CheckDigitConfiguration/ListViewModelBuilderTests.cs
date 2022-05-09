using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.CheckDigitConfiguration.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.CheckDigitConfiguration.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.CheckDigitConfiguration
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ICheckDigitConfigurationService> _mockCheckDigitConfigurationService = new Mock<ICheckDigitConfigurationService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockCheckDigitConfigurationService.Object);
        }

        private void SetupService(Mock<ICheckDigitConfigurationService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.CheckDigitConfiguration.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.CheckDigitConfiguration>()
                {
                    Items = new List<BusinessLogic.Entities.CheckDigitConfiguration>()
                    {
                        new BusinessLogic.Entities.CheckDigitConfiguration()
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
            SetupService(_mockCheckDigitConfigurationService);

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
            SetupService(_mockCheckDigitConfigurationService);

            // Act
            var result = _viewModelBuilder.Build(new Models.CheckDigitConfiguration.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
