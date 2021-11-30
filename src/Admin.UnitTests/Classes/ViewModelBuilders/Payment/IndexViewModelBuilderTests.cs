using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Payment
{
    [TestClass]
    public class IndexViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();

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

        private void SetupUserPostPaymentMopCodeSerivce(Mock<IUserPostPaymentMopCodeService> mockVatService)
        {
            mockVatService.Setup(x => x.GetUserPostPaymentMopCodes(It.IsAny<int>()))
                .Returns(new System.Collections.Generic.List<BusinessLogic.Entities.UserPostPaymentMopCode>()
                {
                    new BusinessLogic.Entities.UserPostPaymentMopCode()
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
            Mock<IFundService> mockFundService = new Mock<IFundService>();
            Mock<IVatService> mockVatService = new Mock<IVatService>();
            Mock<IUserPostPaymentMopCodeService> mockUserPostPaymentMopCodeService = new Mock<IUserPostPaymentMopCodeService>();

            SetupFundService(mockFundService);
            SetupVatSerivce(mockVatService);
            SetupUserPostPaymentMopCodeSerivce(mockUserPostPaymentMopCodeService);

            var indexViewModelBuilder = new Admin.Classes.ViewModelBuilders.Payment.IndexViewModelBuilder(
                _mockLogger.Object,
                mockFundService.Object,
                mockVatService.Object,
                mockUserPostPaymentMopCodeService.Object,
                _mockSecurityContext.Object);

            var result = indexViewModelBuilder.Build();
            result.VatCodes.ToSelectList().Count.Should().Be(1);
        }

        [TestMethod]
        public void PaymentsIndexViewModelBuilderOnBuildPopulatesModel()
        {
            Mock<IFundService> mockFundService = new Mock<IFundService>();
            Mock<IVatService> mockVatService = new Mock<IVatService>();
            Mock<IUserPostPaymentMopCodeService> mockUserPostPaymentMopCodeService = new Mock<IUserPostPaymentMopCodeService>();

            SetupFundService(mockFundService);
            SetupVatSerivce(mockVatService);
            SetupUserPostPaymentMopCodeSerivce(mockUserPostPaymentMopCodeService);

            var indexViewModelBuilder = new Admin.Classes.ViewModelBuilders.Payment.IndexViewModelBuilder(
                _mockLogger.Object,
                mockFundService.Object,
                mockVatService.Object,
                mockUserPostPaymentMopCodeService.Object,
                _mockSecurityContext.Object);

            var result = indexViewModelBuilder.Build(new Admin.Models.Payment.IndexViewModel());

            result.VatCodes.ToSelectList().Count.Should().Be(1);
        }
    }
}
