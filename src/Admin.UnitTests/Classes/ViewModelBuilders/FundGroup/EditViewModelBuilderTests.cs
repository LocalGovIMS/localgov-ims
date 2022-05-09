using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.FundGroup.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundGroup.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundGroupService.Object,
                _mockFundService.Object);
        }

        private void SetupServices()
        {
            _mockFundGroupService.Setup(x => x.GetFundGroup(It.IsAny<int>())).Returns(new BusinessLogic.Entities.FundGroup()
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

            _mockFundService.Setup(x => x.GetAllFunds()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    FundCode= "23"
                }
            });

            _mockFundService.Setup(x => x.GetAllFunds(It.IsAny<bool>())).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
            {
                new BusinessLogic.Entities.Fund()
                {
                    FundCode= "23"
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
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_sets_the_Id_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_FundGroupName_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.FundGroupName.Should().Be("MOCKTEST");
        }
    }
}
