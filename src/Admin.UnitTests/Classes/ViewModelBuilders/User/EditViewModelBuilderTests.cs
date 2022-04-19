using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ViewModel = Admin.Models.User.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.User.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.User
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IOfficeService> _mockOfficeService = new Mock<IOfficeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockOfficeService.Object);
        }

        private void SetupUserService(Mock<IUserService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.User()
                {
                    UserId = 123,
                    UserName = "MockUserEdit",
                    LastLogin = DateTime.Now,
                    ExpiryDays = 30,
                    Disabled = false,
                    OfficeCode = "SP"
                });
        }

        private void SetupOfficeService(Mock<IOfficeService> service)
        {
            service.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.Office>());

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
            SetupOfficeService(_mockOfficeService);

            // Act
            var result = _viewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
