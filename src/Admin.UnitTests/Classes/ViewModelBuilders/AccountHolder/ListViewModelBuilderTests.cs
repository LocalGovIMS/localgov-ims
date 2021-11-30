using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private void SetupAccountHolderService(Mock<IAccountHolderService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.AccountHolder.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.AccountHolder>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.AccountHolder>()
                {
                    new BusinessLogic.Entities.AccountHolder() {
                        AccountReference = "123",
                        AddressLine1 = string.Empty,
                        AddressLine2 = string.Empty,
                        AddressLine3 = string.Empty,
                        AddressLine4 = string.Empty,
                        CurrentBalance = 0,
                        Forename = string.Empty,
                        Fund = null,
                        FundCode = string.Empty,
                        LastUpdated = null,
                        PeriodCredit = 0,
                        PeriodDebit = 0,
                        Postcode = string.Empty,
                        RecordType = string.Empty,
                        SurnameSoundex = string.Empty,
                        StopMessageReference = string.Empty,
                        Surname = string.Empty,
                        Title = string.Empty,
                        UserField1 = string.Empty,
                        UserField2 = string.Empty,
                        UserField3 = string.Empty,
                    }
                },
                Page = 1,
                PageSize = 20
            });
        }


        private Admin.Models.AccountHolder.SearchViewModel GetSearchCriteriaPopulated()
        {
            return new Admin.Models.AccountHolder.SearchViewModel()
            {
                AccountReference = "775326912",
                FundCode = "23",
                HouseNumberName = "1",
                Page = 1,
                PostCode = "S73 0PS",
                Street = "SCHOOL STREET",
                Surname = "HARTLEY"
            };
        }


        [TestMethod]
        public void OnBuildWithParamsPageIs0ReturnsViewModel()
        {
            // Arrange
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();
            Mock<IFundService> mockFundService = new Mock<IFundService>();


            mockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
            {
                FundName = "Test Fund"
            });

            SetupAccountHolderService(acountHolderService);

            var viewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.ListViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object,
                mockFundService.Object);

            // Act
            var result = viewModelBuilder.Build(new Admin.Models.AccountHolder.SearchViewModel()
            {
                AccountReference = "775326912",
                FundCode = "23",
                HouseNumberName = "1",
                Page = 0,
                PostCode = "S73 0PS",
                Street = "SCHOOL STREET",
                Surname = "HARTLEY"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admin.Models.AccountHolder.ListViewModel));
        }



        [TestMethod]
        public void OnBuildWithParamsPageIs1ReturnsViewModel()
        {
            // Arrange
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();
            Mock<IFundService> mockFundService = new Mock<IFundService>();


            mockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
            {
                FundName = "Test Fund"
            });

            SetupAccountHolderService(acountHolderService);

            var viewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.ListViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object,
                mockFundService.Object);

            // Act
            var result = viewModelBuilder.Build(GetSearchCriteriaPopulated());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admin.Models.AccountHolder.ListViewModel));
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

            SetupAccountHolderService(acountHolderService);

            var viewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.ListViewModelBuilder(
                _mockLogger.Object,
                acountHolderService.Object,
                _mockFundService.Object);

            // Act
            var result = viewModelBuilder.Build();
            result.Should().BeOfType(typeof(Admin.Models.AccountHolder.ListViewModel)).And.Subject.Should().NotBeNull();
        }

        //[TestMethod]
        //public void OnBuildReturnsViewModelWithAccountHolder()
        //{
        //    // Arrange
        //    Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

        //    SetupAccountHolderService(acountHolderService);

        //    var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder(
        //        _mockLogger.Object,
        //        acountHolderService.Object);

        //    // Act
        //    var result = detailsViewModelBuilder.Build("123");

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(((Admin.Models.AccountHolder.DetailsViewModel)result).AccountHolder);
        //}

        //[TestMethod]
        //public void OnBuildReturnsNull()
        //{
        //    // Arrange
        //    Mock<IAccountHolderService> acountHolderService = new Mock<IAccountHolderService>();

        //    SetupAccountHolderService(acountHolderService);

        //    var detailsViewModelBuilder = new Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder(
        //        _mockLogger.Object,
        //        acountHolderService.Object);

        //    // Act
        //    var result = detailsViewModelBuilder.Build();

        //    // Assert
        //    Assert.IsNull(result);
        //}
    }
}
