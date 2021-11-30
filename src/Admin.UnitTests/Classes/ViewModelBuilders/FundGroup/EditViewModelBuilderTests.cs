using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        [TestMethod]
        public void TestFundGroupEditViewModelOnBuildReturnsNull()
        {
            var editViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.EditViewModelBuilder
                (_mockLogger.Object, _mockFundGroupService.Object, _mockFundService.Object);

            var result = editViewModelBuilder.Build();
            result.Should().BeNull();

        }

        [TestMethod]

        public void TestFundGroupEditViewModelOnBuildReturnModel()
        {
            Mock<IFundGroupService> mockFundGroupService = new Mock<IFundGroupService>();

            Mock<IFundService> mockFundService = new Mock<IFundService>();


            mockFundGroupService.Setup(x => x.GetFundGroup(It.IsAny<int>())).Returns(new BusinessLogic.Entities.FundGroup()
            {
                FundGroupId = 1,
                Name = "MOCKTEST",
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


            mockFundService.Setup(x => x.GetAllFunds()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    FundCode= "23"
                }
            });

            mockFundService.Setup(x => x.GetAllFunds(It.IsAny<bool>())).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    FundCode= "23"
                }
            });

            var editViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.EditViewModelBuilder
               (_mockLogger.Object, mockFundGroupService.Object, mockFundService.Object);

            var result = editViewModelBuilder.Build(2);
            result.FundGroupName.Should().Be("MOCKTEST");
        }
    }
}
