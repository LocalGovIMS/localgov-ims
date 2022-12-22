using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturnTemplateRow.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplateRow.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplateRow
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateRowService> _mockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object,
                _mockVatService.Object);
        }

        private void SetupServices()
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
        public void Build_sets_the_EReturnTemplateId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.EReturnTemplateId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_Reference_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Reference.Should().Be("Reference");
        }

        [TestMethod]
        public void Build_sets_the_ReferenceOverride_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ReferenceOverride.Should().Be(false);
        }

        [TestMethod]
        public void Build_sets_the_VatCode_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.VatCode.Should().Be("V1");
        }

        [TestMethod]
        public void Build_sets_the_VatCodeOverride_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.VatOverride.Should().Be(false);
        }

        [TestMethod]
        public void Build_sets_the_Description_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Description.Should().Be("Description");
        }

        [TestMethod]
        public void Build_sets_the_DescriptionOverride_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.DescriptionOverride.Should().Be(false);
        }
    }
}
