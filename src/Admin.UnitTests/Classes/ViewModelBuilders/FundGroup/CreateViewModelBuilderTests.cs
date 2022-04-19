using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.FundGroup.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundGroup.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundGroup
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);
        }

        private void SetupServices()
        {
            _mockFundService.Setup(x => x.GetAllFunds())
                .Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Fund>()
                {
                    new BusinessLogic.Entities.Fund()
                    {
                        FundCode= "23",
                        FundName="TestFund"
                    }
                });
        }

        [TestMethod]
        public void Build_without_arguments_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
