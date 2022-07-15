using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ViewModel = Admin.Models.Import.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Import.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Import
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportService> _mockImportService = new Mock<IImportService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportService.Object);
        }

        private void SetupService()
        {
            _mockImportService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.Import()
                {
                    Id = 1, 
                    ImportType = new BusinessLogic.Entities.ImportType()
                    {
                        Name = "Transaction Import Type Name"
                    },
                    CreatedByUser = new BusinessLogic.Entities.User()
                    {
                        UserName = "Test User"
                    },
                    CreatedDate = DateTime.Now,
                    EventLogs = new List<BusinessLogic.Entities.ImportEventLog>(),
                    StatusHistories = new List<BusinessLogic.Entities.ImportStatusHistory>() { 
                        new BusinessLogic.Entities.ImportStatusHistory()
                        {
                            StatusId = (int)ImportStatusEnum.Received,
                            CreatedDate = DateTime.Now,
                            CreatedByUser = new BusinessLogic.Entities.User()
                            {
                                UserName = "Test User"
                            }
                        }
                    }
                    ,
                    Notes = "Notes",
                    NumberOfRows = 2
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
            SetupService();

            // Act
             var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
