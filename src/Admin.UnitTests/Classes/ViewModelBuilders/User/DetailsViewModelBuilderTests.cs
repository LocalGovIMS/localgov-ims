using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Admin.UnitTests.Classes.ViewModelBuilders.User
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();

        [TestMethod]
        public void TestUserDetailsViewModelThrowsException()
        {
            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.User.DetailsViewModelBuilder
                (_mockLogger.Object, _mockUserService.Object);

            var result = detailsViewModelBuilder.Build();
            result.Should().BeNull();
        }


        [TestMethod]
        public void TestUserDetailsViewModel()
        {
            Mock<IUserService> mockUserService = new Mock<IUserService>();

            mockUserService.Setup(x => x.GetUser(It.IsAny<int>())).Returns(new BusinessLogic.Entities.User()
            {
                UserId = 123,
                UserName = "MockUserDetails",
                LastLogin = DateTime.Now,
                ExpiryDays = 30,
                Disabled = false

            });
            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.User.DetailsViewModelBuilder
                (_mockLogger.Object, mockUserService.Object);

            var result = detailsViewModelBuilder.Build(2);

            result.UserName.Should().Be("MockUserDetails");
        }
    }
}
