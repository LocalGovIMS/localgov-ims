using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.EReturnTemplateRow.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplateRow.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplateRow
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateRowService> _mockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);
        }

        private void SetupService()
        {
            _mockEReturnTemplateRowService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.TemplateRow()
                {
                    Id = 1,
                    TemplateId = 1,
                    Reference = "Reference",
                    ReferenceOverride = false,
                    Description = "Description",
                    DescriptionOverride = false,
                    VatCode = "V1",
                    VatOverride = false
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
            SetupService();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
