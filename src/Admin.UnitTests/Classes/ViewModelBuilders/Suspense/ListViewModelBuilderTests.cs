using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Suspense;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ViewModel = Admin.Models.Suspense.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Suspense.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Suspense
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISuspenseService> _mockSuspenseService = new Mock<ISuspenseService>();

        private void SetupSuspenseService(Mock<ISuspenseService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<SuspenseWrapper>()
                {
                    Count = 0,
                    Page = page == 0 ? 1 : page,
                    PageSize = 20,
                    Items = new List<SuspenseWrapper>() { new SuspenseWrapper(
                        new BusinessLogic.Entities.Suspense() {
                            Id = 1
                        })
                    }
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupSuspenseService(_mockSuspenseService, 1);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupSuspenseService(_mockSuspenseService, 1);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseService.Object);

            var result = viewModelBuidler.Build(new Admin.Models.Suspense.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupSuspenseService(_mockSuspenseService, 1);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseService.Object);

            var result = viewModelBuidler.Build(new Admin.Models.Suspense.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
