using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();

        [TestMethod]
        public void TestFundGroupDetailsViewModelThrowsException()
        {
            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.DetailsViewModelBuilder
                (_mockLogger.Object, _mockFundGroupService.Object);

            var result = detailsViewModelBuilder.Build();
            result.Should().BeNull();
        }

        [TestMethod]
        public void TestFundGroupDetailsViewModelOnBuild()
        {
            Mock<IFundGroupService> mockFundGroupService = new Mock<IFundGroupService>();

            mockFundGroupService.Setup(x => x.GetFundGroup(It.IsAny<int>())).Returns(new BusinessLogic.Entities.FundGroup()
            {
                FundGroupId = 1,
                Name = "MockFundGroupName",
                FundGroupFunds = new System.Collections.Generic.List<BusinessLogic.Entities.FundGroupFund>()
                {
                    {
                        new BusinessLogic.Entities.FundGroupFund()
                        {
                            Fund= new BusinessLogic.Entities.Fund(){FundCode="123",FundName="TESTFUND"},
                            FundCode = "23",
                            FundGroup= new BusinessLogic.Entities.FundGroup(){FundGroupId=1},
                            FundGroupId=1,
                            FundGroupFundId=1
                        }
                    }
                }
            });


            var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.DetailsViewModelBuilder
                (_mockLogger.Object, mockFundGroupService.Object);

            var result = detailsViewModelBuilder.Build(2);

            result.FundGroupName.Should().Be("MockFundGroupName");
        }

    }
}
