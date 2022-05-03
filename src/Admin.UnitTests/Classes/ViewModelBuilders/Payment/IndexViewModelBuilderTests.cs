using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Payment.IndexViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Payment.IndexViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Payment
{
    [TestClass]
    public class IndexViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();
        private readonly Mock<IUserMethodOfPaymentService> _mockUserMethodOfPaymentService = new Mock<IUserMethodOfPaymentService>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object,
                _mockUserMethodOfPaymentService.Object,
                _mockSecurityContext.Object);
        }

        private void SetupFundService(Mock<IFundService> mockFundService)
        {
            mockFundService.Setup(x => x.GetAllFunds()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    VatOverride= false,
                    VatCode="1",
                    FundCode= "23",
                    AccountExist=true
                }
            });
        }

        private void SetupVatSerivce(Mock<IVatService> mockVatService)
        {
            mockVatService.Setup(x => x.GetAllCodes()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Vat>()
            {
                new BusinessLogic.Entities.Vat()
                {
                    Percentage=0,
                    VatCode="23"
                }
            });
        }

        private void SetupUserMethodOfPaymentSerivce(Mock<IUserMethodOfPaymentService> mockVatService)
        {
            mockVatService.Setup(x => x.GetByUserId(It.IsAny<int>()))
                .Returns(new System.Collections.Generic.List<BusinessLogic.Entities.UserMethodOfPayment>()
                {
                    new BusinessLogic.Entities.UserMethodOfPayment()
                    {
                        Id = 1,
                        MopCode = "23",
                        Mop = new BusinessLogic.Entities.Mop () {
                            MopName = "Test"
                        }
                    }
                });
        }

        [TestMethod]
        public void PaymentsIndexViewModelBuilderOnBuildReturnsModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatSerivce(_mockVatService);
            SetupUserMethodOfPaymentSerivce(_mockUserMethodOfPaymentService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.VatCodes.ToSelectList().Count.Should().Be(1);
        }

        [TestMethod]
        public void PaymentsIndexViewModelBuilderOnBuildPopulatesModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatSerivce(_mockVatService);
            SetupUserMethodOfPaymentSerivce(_mockUserMethodOfPaymentService);

            // Act
            var result = _viewModelBuilder.Build(new ViewModel());

            // Assert
            result.VatCodes.ToSelectList().Count.Should().Be(1);
        }
    }
}
