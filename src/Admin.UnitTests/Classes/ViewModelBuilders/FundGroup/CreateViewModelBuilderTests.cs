using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        [TestMethod]
        public void TestCreateViewModelBuilderOnBuildReturnsModel()
        {
            Mock<IFundService> mockFundService = new Mock<IFundService>();

            mockFundService.Setup(x => x.GetAllFunds()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    FundCode= "23",
                    FundName="TestFund"
                }
            });

            var createViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.CreateViewModelBuilder(
                _mockLogger.Object,
                mockFundService.Object);

            var result = createViewModelBuilder.Build();
            result.Funds.Count.Should().Be(1);
        }


        [TestMethod]
        public void TestCreateDetailsViewModelThrowsException()
        {
            var createViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.CreateViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);

            var result = createViewModelBuilder.Build(2);
            result.Should().BeNull();
        }
    }
}
