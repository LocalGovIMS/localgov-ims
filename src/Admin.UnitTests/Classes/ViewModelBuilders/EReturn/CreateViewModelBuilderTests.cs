using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.EReturn.CreateViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturn.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturn
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITemplateService> _mockTemplateService = new Mock<ITemplateService>();
        private readonly Mock<IEReturnTypeService> _mockEReturnTypeService = new Mock<IEReturnTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTemplateService.Object,
                _mockEReturnTypeService.Object);
        }

        private void SetupTemplateService(Mock<ITemplateService> service)
        {
            service.Setup(x => x.GetAllTemplates()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.Template>()
            {
                new BusinessLogic.Entities.Template()
                {
                    Id = 1,
                    Name = "Template1"
                }
            });
        }

        private void SetupEReturnTypeService(Mock<IEReturnTypeService> service)
        {
            service.Setup(x => x.GetAllEReturnTypes()).Returns(new System.Collections.Generic.List<BusinessLogic.Entities.EReturnType>()
            {
                new BusinessLogic.Entities.EReturnType()
                {
                    Id = 1,
                    Name = "Type1"
                }
            });
        }

        [TestMethod]
        public void OnBuildReturnsModel()
        {
            // Arrange
            SetupTemplateService(_mockTemplateService);
            SetupEReturnTypeService(_mockEReturnTypeService);

            // Act
            var result = _viewModelBuilder.Build();

            //Assert
            result.Should().BeOfType<ViewModel>();
        }

        [TestMethod]
        public void BuildIssueReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnRebuildReturnsSameModel()
        {
            // Arrange
            SetupTemplateService(_mockTemplateService);
            SetupEReturnTypeService(_mockEReturnTypeService);

            var model = new ViewModel()
            {
                TemplateId = 987
            };

            // Act
            var result = _viewModelBuilder.Rebuild(model);

            //Assert
            result.Should().BeOfType<ViewModel>();
            result.TemplateId.Should().Be(987);
        }
    }
}
