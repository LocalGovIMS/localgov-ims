using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturnNote.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnNote.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnNote
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnNoteService> _mockEReturnNoteService = new Mock<IEReturnNoteService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnNoteService.Object);
        }

        private void SetupEReturnNoteService(Mock<IEReturnNoteService> service)
        {
            service.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(new List<BusinessLogic.Entities.EReturnNote>() {
                    {
                        new BusinessLogic.Entities.EReturnNote()
                        {
                            Note = "A Note"
                        }
                    }});
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            SetupEReturnNoteService(_mockEReturnNoteService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupEReturnNoteService(_mockEReturnNoteService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
