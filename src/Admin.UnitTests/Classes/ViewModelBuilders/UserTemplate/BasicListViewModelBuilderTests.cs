using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Shared.BasicListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserTemplate.BasicListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.UserTemplate
{
    [TestClass]
    public class BasicListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserTemplateService> _mockUserTemplateService = new Mock<IUserTemplateService>();

        private void SetupUserTemplateService(Mock<IUserTemplateService> service)
        {
            service.Setup(x => x.GetUserTemplates(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserTemplate>()
            {
                {
                    new BusinessLogic.Entities.UserTemplate() {
                        UserTemplateId = 0,
                        UserId = 0,
                        TemplateId = 0,
                        Template = new BusinessLogic.Entities.Template() {
                            Name = "Test1"
                        }
                    }
                },
                {
                    new BusinessLogic.Entities.UserTemplate() {
                        UserTemplateId = 1,
                        UserId = 1,
                        TemplateId = 1,
                        Template = new BusinessLogic.Entities.Template() {
                            Name = "Test2"
                        }
                    }
                }
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var listViewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockUserTemplateService.Object);

            // Act
            var result = listViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupUserTemplateService(_mockUserTemplateService);

            var listViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserTemplateService.Object);

            var result = listViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
