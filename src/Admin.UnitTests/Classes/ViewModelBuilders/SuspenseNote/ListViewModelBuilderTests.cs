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

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseNoteService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupSuspenseNoteService(_mockSuspenseNoteService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseNoteService.Object);

            // Act
            var result = viewModelBuidler.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
