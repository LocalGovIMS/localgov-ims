using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.SuspenseNote.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.SuspenseNote.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.SuspenseNote
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISuspenseNoteService> _mockSuspenseNoteService = new Mock<ISuspenseNoteService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseNoteService.Object);
        }

        private void SetupSuspenseNoteService(Mock<ISuspenseNoteService> service)
        {
            service.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(new List<BusinessLogic.Entities.SuspenseNote>() {
                    {
                        new BusinessLogic.Entities.SuspenseNote()
                        {
                            Note = "A Note"
                        }
                    }});
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            SetupSuspenseNoteService(_mockSuspenseNoteService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupSuspenseNoteService(_mockSuspenseNoteService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
