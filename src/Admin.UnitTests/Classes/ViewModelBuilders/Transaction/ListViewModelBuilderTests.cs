using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Transaction.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Transaction.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Transaction
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionService> _mockTransactionService = new Mock<ITransactionService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IMethodOfPaymentService> _mockMopService = new Mock<IMethodOfPaymentService>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionService.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockUserService.Object);
        }

        private void SetupTransactionService(Mock<ITransactionService> service, int page)
        {
            service.Setup(x => x.SearchTransactions(It.IsAny<BusinessLogic.Models.Transactions.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Models.Transactions.SearchResultItem>()
                {
                    Count = 0,
                    Page = page == 0 ? 1 : page,
                    PageSize = 20,
                    Items = new List<BusinessLogic.Models.Transactions.SearchResultItem>()
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
        }

        private void SetupMopService(Mock<IMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetAllMops()).Returns(
                new List<BusinessLogic.Entities.Mop>()
                {
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M1",
                            MopName = "Mop1"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M2",
                            MopName = "Mop2"
                        }
                    }
                }

            );

            service.Setup(x => x.GetAllMops(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Mop>()
                {
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M1",
                            MopName = "Mop1"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M2",
                            MopName = "Mop2"
                        }
                    }
                }

            );
        }

        private void SetupUserService(Mock<IUserService> service)
        {
            service.Setup(x => x.GetAllUsers()).Returns(
                new List<BusinessLogic.Entities.User>()
                {
                    {
                        new BusinessLogic.Entities.User()
                        {
                            UserId = 1,
                            DisplayName = "User 1"
                        }
                    },
                    {
                        new BusinessLogic.Entities.User()
                        {
                            UserId = 2,
                            DisplayName = "User 2"
                        }
                    }
                }

            );
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupTransactionService(_mockTransactionService, 1);
            SetupFundService(_mockFundService);
            SetupMopService(_mockMopService);
            SetupUserService(_mockUserService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionService.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockUserService.Object);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange

            SetupTransactionService(_mockTransactionService, 0);
            SetupFundService(_mockFundService);
            SetupMopService(_mockMopService);
            SetupUserService(_mockUserService);

            var result = _viewModelBuilder.Build(new Models.Transaction.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupTransactionService(_mockTransactionService, 1);
            SetupFundService(_mockFundService);
            SetupMopService(_mockMopService);
            SetupUserService(_mockUserService);

            var result = _viewModelBuilder.Build(new Models.Transaction.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
