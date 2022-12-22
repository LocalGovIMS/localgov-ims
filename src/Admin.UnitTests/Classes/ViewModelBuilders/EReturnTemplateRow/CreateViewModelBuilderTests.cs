using Admin.Classes.ViewModelBuilders.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturnTemplateRow.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplateRow.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplateRow
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);
        }

        private void SetupServices()
        {
            _mockVatService.Setup(x => x.GetAllCodes())
                .Returns(new List<BusinessLogic.Entities.Vat>()
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "V1"
                        },
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "V2"
                        },
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "V3"
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
            var result = _viewModelBuilder.Build(new CreateViewModelBuilderArgs() { EReturnTemplateId = 1 });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
