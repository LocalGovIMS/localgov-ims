using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.UserTemplate.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserTemplate.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.UserTemplate

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IEReturnTemplateService> _mockEReturnTemplateService = new Mock<IEReturnTemplateService>();
        private readonly Mock<IUserTemplateService> _mockUserTemplateService = new Mock<IUserTemplateService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockEReturnTemplateService.Object,
                _mockUserTemplateService.Object);
        }

        private void SetupUserService(Mock<IUserService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<int>())).Returns(new BusinessLogic.Entities.User()
            {
                Disabled = false,
                DisplayName = string.Empty,
                ExpiryDays = 0,
                LastLogin = null,
                UserFundGroups = null,
                UserId = 0,
                UserName = string.Empty,
                UserRoles = null
            });
        }

        private void SetupTemplateService(Mock<IEReturnTemplateService> service)
        {
            service.Setup(x => x.GetAll()).Returns(new List<BusinessLogic.Entities.Template>()
            {
                {
                    new BusinessLogic.Entities.Template() {
                        Id = 1
                    }
                },
                {
                    new BusinessLogic.Entities.Template() {
                        Id = 2
                    }
                }
            });
        }

        private void SetupUserTemplateService(Mock<IUserTemplateService> service)
        {
            service.Setup(x => x.GetByUserId(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserTemplate>()
            {
                {
                    new BusinessLogic.Entities.UserTemplate() {
                        UserTemplateId = 0,
                        UserId = 0,
                        TemplateId = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserTemplate() {
                        UserTemplateId = 1,
                        UserId = 1,
                        TemplateId = 1
                    }
                }
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupTemplateService(_mockEReturnTemplateService);
            SetupUserTemplateService(_mockUserTemplateService);

            // Act
            var result = _viewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserReturnsViewModel()
        {
            // Arrange
            SetupTemplateService(_mockEReturnTemplateService);
            SetupUserTemplateService(_mockUserTemplateService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserRolesReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupTemplateService(_mockEReturnTemplateService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
