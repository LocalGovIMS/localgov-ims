using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ViewModel = Admin.Models.AccountHolder.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountHolder.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountHolderService.Object,
               _mockFundService.Object);
        }

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
                        FundMessageId = null,
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

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetAllFunds()).Returns(
                new List<BusinessLogic.Entities.Fund>()
                {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1",
                            FundName = "Fund1"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F2",
                            FundName = "Fund2"
                        }
                    }
                }
            );

            service.Setup(x => x.GetAllFunds(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Fund>()
                {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1",
                            FundName = "Fund1"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F2",
                            FundName = "Fund2"
                        }
                    }
                }
            );

            service.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
            {
                FundName = "Test Fund"
            });
        }

        private Admin.Models.AccountHolder.SearchCriteria GetSearchCriteriaPopulated()
        {
            return new Admin.Models.AccountHolder.SearchCriteria()
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
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);
            
            var searchCriteria = GetSearchCriteriaPopulated();
            searchCriteria.Page = 0;

            // Act
            var result = _viewModelBuilder.Build(searchCriteria);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamsPageIs1ReturnsViewModel()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build(GetSearchCriteriaPopulated());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel)).And.Subject.Should().NotBeNull();
        }
    }
}
