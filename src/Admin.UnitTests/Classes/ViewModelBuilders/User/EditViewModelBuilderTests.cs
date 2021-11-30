using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Admin.UnitTests.Classes.ViewModelBuilders.User
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IOfficeService> _mockOfficeService = new Mock<IOfficeService>();

        [TestMethod]
        public void TestUserEditViewModelOnBuildReturnsNull()
        {
            var editViewModelBuilder = new Admin.Classes.ViewModelBuilders.User.EditViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockOfficeService.Object);

            var result = editViewModelBuilder.Build();
            result.Should().BeNull();

        }


        [TestMethod]
        public void TestUserEditViewModelOnBuildWithParamReturnsNewModel()
        {
            var editViewModelBuilder = new Admin.Classes.ViewModelBuilders.User.EditViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockOfficeService.Object);

            var result = editViewModelBuilder.Build(2);
            result.UserName.Should().BeNull();
        }

        [TestMethod]
        public void TestUserEditViewModel()
        {
            Mock<IUserService> mockUserService = new Mock<IUserService>();
            Mock<IOfficeService> mockOfficeService = new Mock<IOfficeService>();

            mockUserService.Setup(x => x.GetUser(It.IsAny<int>())).Returns(new BusinessLogic.Entities.User()
            {
                UserId = 123,
                UserName = "MockUserEdit",
                LastLogin = DateTime.Now,
                ExpiryDays = 30,
                Disabled = false,
                OfficeCode = "SP"
            });
            mockOfficeService.Setup(x => x.GetOffices()).Returns(new List<Office>());

            var editViewModelBuilder = new Admin.Classes.ViewModelBuilders.User.EditViewModelBuilder(
                _mockLogger.Object,
                mockUserService.Object,
                mockOfficeService.Object);

            var result = editViewModelBuilder.Build(2);

            result.UserName.Should().Be("MockUserEdit");
        }
    }
}
