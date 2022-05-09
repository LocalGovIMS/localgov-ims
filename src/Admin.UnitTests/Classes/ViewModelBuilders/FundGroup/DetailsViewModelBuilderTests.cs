using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.FundGroup.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundGroup.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundGroupService.Object);
        }

        private void SetupService(Mock<IFundGroupService> service)
        {
            service.Setup(x => x.GetFundGroup(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.FundGroup()
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
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockFundGroupService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
