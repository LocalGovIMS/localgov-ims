using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.SystemMessage.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.SystemMessage.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.SystemMessage

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISystemMessageService> _mockSystemMessageService = new Mock<ISystemMessageService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSystemMessageService.Object);
        }

        private void SetupService(Mock<ISystemMessageService> service)
        {
            service.Setup(x => x.GetSystemMessage(It.IsAny<int>()))
                .Returns(TestData.GetASystemMesssage());

            service.Setup(x => x.GetSeverities())
                .Returns(TestData.GetSeverities());
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
            SetupService(_mockSystemMessageService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
