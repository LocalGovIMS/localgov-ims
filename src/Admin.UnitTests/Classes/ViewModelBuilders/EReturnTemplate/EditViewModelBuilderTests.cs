using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturnTemplate.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplate.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplate
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateService> _mockEReturnTemplateService = new Mock<IEReturnTemplateService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object,
                _mockEReturnTemplateService.Object);
        }

        private void SetupEReturnTemplateService(Mock<IEReturnTemplateService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.Template()
                {
                    Id = 1
                });
        }

        private void SetupVatService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(
                new List<BusinessLogic.Entities.Vat>()
                {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "v1"
                        }
                    }
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullEReturnTemplateReturnsViewModel()
        {
            // Arrange
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModel()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithVatCodes()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.VatCodes.Should().NotBeNull();
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithAccountReferenceValidators()
        {
            // Arrange
            SetupEReturnTemplateService(_mockEReturnTemplateService);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.VatCodes.Should().NotBeNull();
        }
    }
}
