using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturnTemplate.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplate.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplate
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateService> _mockEReturnTemplateService = new Mock<IEReturnTemplateService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnTemplateService.Object);
        }

        private void SetupEReturnTemplateService(Mock<IEReturnTemplateService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.EReturnTemplate.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.Template>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.Template>() {
                    {
                        new BusinessLogic.Entities.Template()
                        {
                            Id = 1
                        }
                    }
                },
                Page = 1,
                PageSize = 20
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService, 1);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService, 0);

            // Act
            var result = _viewModelBuilder.Build(new Models.EReturnTemplate.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService, 1);

            // Act
            var result = _viewModelBuilder.Build(new Models.EReturnTemplate.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
