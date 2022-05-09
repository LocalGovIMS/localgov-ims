using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.UserMethodOfPayment.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserMethodOfPayment.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.UserMethodOfPayment

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IMethodOfPaymentService> _mockMethodOfPaymentService = new Mock<IMethodOfPaymentService>();
        private readonly Mock<IUserMethodOfPaymentService> _mockUserMethodOfPaymentService = new Mock<IUserMethodOfPaymentService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockMethodOfPaymentService.Object,
                _mockUserMethodOfPaymentService.Object);
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
                UserMethodOfPayments = null
            });
        }

        private void SetupMethodOfPaymentService(Mock<IMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetAllMops()).Returns(new List<BusinessLogic.Entities.Mop>()
            {
                {
                    new BusinessLogic.Entities.Mop() {
                        MopName = "Mop 1",
                        MopCode = "01",
                        UserMethodOfPayments = null
                    }
                },
                {
                    new BusinessLogic.Entities.Mop() {
                        MopName = "Mop 2",
                        MopCode = "02",
                        UserMethodOfPayments = null
                    }
                }
            });
        }

        private void SetupUserMethodOfPaymentService(Mock<IUserMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetByUserId(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserMethodOfPayment>()
            {
                {
                    new BusinessLogic.Entities.UserMethodOfPayment() {
                        Mop = new BusinessLogic.Entities.Mop(),
                        MopCode = "01",
                        User = null,
                        UserId = 0,
                        Id = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserMethodOfPayment() {
                        Mop = new BusinessLogic.Entities.Mop(),
                        MopCode = "01",
                        User = null,
                        UserId = 0,
                        Id = 1
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
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);
            SetupUserMethodOfPaymentService(_mockUserMethodOfPaymentService);

            // Act
            var result = _viewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserReturnsViewModel()
        {
            // Arrange
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);
            SetupUserMethodOfPaymentService(_mockUserMethodOfPaymentService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserMethodOfPaymentsReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
