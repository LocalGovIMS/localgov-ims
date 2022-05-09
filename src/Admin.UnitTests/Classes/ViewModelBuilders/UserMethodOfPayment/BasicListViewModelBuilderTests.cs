using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Shared.BasicListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserMethodOfPayment.BasicListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.UserMethodOfPayment
{
    [TestClass]
    public class BasicListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserMethodOfPaymentService> _mockUserMethodOfPaymentService = new Mock<IUserMethodOfPaymentService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserMethodOfPaymentService.Object);
        }

        private void SetupUserMethodOfPaymentService(Mock<IUserMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetByUserId(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserMethodOfPayment>()
            {
                {
                    new BusinessLogic.Entities.UserMethodOfPayment() {
                        Mop = new BusinessLogic.Entities.Mop() { MopName = "MOP 1" },
                        MopCode = "01",
                        User = null,
                        UserId = 0,
                        Id = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserMethodOfPayment() {
                        Mop = new BusinessLogic.Entities.Mop() { MopName = "MOP 2" },
                        MopCode = "02",
                        User = null,
                        UserId = 0,
                        Id = 2
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
            SetupUserMethodOfPaymentService(_mockUserMethodOfPaymentService);

            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
