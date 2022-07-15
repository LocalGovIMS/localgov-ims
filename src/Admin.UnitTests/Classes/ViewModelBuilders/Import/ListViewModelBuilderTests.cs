using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Import.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Import.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Import
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportService> _mockImportService = new Mock<IImportService>();
        private readonly Mock<IImportTypeService> _mockImportTypeService = new Mock<IImportTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportService.Object,
                _mockImportTypeService.Object);
        }

        private void SetupServices()
        {
            _mockImportService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.Import.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.Import>()
                {
                    Items = new List<BusinessLogic.Entities.Import>()
                    {
                        new BusinessLogic.Entities.Import()
                        {
                            Id = 1
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
                });

            _mockImportTypeService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.ImportType>()
                {
                    new BusinessLogic.Entities.ImportType()
                    {
                        Id = 1,
                        Name = "Transaction Import Type"
                    }
                });
        }

        [TestMethod]
        public void Build_without_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupServices();

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
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(new Models.Import.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
