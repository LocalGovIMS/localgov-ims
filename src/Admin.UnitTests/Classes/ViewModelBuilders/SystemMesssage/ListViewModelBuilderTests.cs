using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ViewModel = Admin.Models.SystemMessage.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.SystemMessage.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.SystemMessage
{
    [TestClass]
    public class ListViewModelBuilderTests
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
            service.Setup(x => x.GetSystemMessages()).Returns(
                new List<BusinessLogic.Entities.SystemMessage>()
                {
                    {
                        TestData.GetASystemMesssage()
                    }
                });

            service.Setup(x => x.GetSeverities())
                .Returns(TestData.GetSeverities());
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupService(_mockSystemMessageService);

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
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }
    }
}
