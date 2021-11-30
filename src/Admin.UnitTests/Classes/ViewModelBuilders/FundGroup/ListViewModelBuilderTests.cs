using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class ListViewModelBuilderTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();


        [TestMethod]
        public void FundGroupViewModelbuilderOnBuildReturnsList()
        {
            Mock<IFundGroupService> mockFundGroupService = new Mock<IFundGroupService>();

            mockFundGroupService.Setup(x => x.GetAllFundGroups()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.FundGroup>()
            {
                new BusinessLogic.Entities.FundGroup()
                {
                    FundGroupId=1,
                    Name="MOCKFundGroup"
                }
            });

            var listViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.ListViewModelBuilder
               (_mockLogger.Object, mockFundGroupService.Object);

            var result = listViewModelBuilder.Build();
            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void FundGroupListViewModelbuilderOnBuildReturnsNull()
        {
            var listViewModelBuilder = new Admin.Classes.ViewModelBuilders.FundGroup.ListViewModelBuilder
                  (_mockLogger.Object, _mockFundGroupService.Object);

            var result = listViewModelBuilder.Build(2);
            result.Should().BeNull();
        }
    }
}
