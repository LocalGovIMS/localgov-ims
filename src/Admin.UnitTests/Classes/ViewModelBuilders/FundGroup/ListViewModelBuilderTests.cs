using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.FundGroup.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundGroup.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class ListViewModelBuilderTests
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

        private void SetupFundGroupService(Mock<IFundGroupService> service)
        {
            service.Setup(x => x.GetAllFundGroups())
                .Returns(new List<BusinessLogic.Entities.FundGroup>()
                {
                    new BusinessLogic.Entities.FundGroup()
                    {
                        FundGroupId=1,
                        Name="MOCKFundGroup"
                    }
                });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupFundGroupService(_mockFundGroupService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType<List<ViewModel>>();
        }

        [TestMethod]
        public void OnBuildWithNoDataReturnsNull()
        {
            // Arrange          

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeNull();
        }
    }
}
