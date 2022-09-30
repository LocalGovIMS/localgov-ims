using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Suspense.JournalViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Suspense.JournalViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Journal
{
    [TestClass]
    public class JournalViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IMethodOfPaymentService> _mockMopService = new Mock<IMethodOfPaymentService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockVatService.Object);
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
                            FundName = "Fund1",
                            VatCode = "W0"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F2",
                            FundName = "Fund2",
                            VatCode = "N0"
                        }
                    }
                }

            );

            service.Setup(x => x.GetCreditNoteFunds()).Returns(
                new List<BusinessLogic.Entities.Fund>()
                {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1",
                            FundName = "Fund1",
                            VatCode = "W0"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F2",
                            FundName = "Fund2",
                            VatCode = "N0"
                        }
                    }
                }

            );
        }

        private void SetupVatService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(
                new List<BusinessLogic.Entities.Vat>()
                {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "W0",
                            Percentage = (decimal) 0.0200
                        }
                    },
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "N0",
                            Percentage = (decimal) 0.0000
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
                   new BusinessLogic.Entities.Mop()
                   {
                       MopCode="test1",
                       MopName="testMOP"
                   }

                });

            service.Setup(x => x.GetAllMops(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Mop>()
                {
                    new BusinessLogic.Entities.Mop()
                    {
                        MopCode="JR",
                        MopName="Journal Reallocation",
                        Metadata = new List<BusinessLogic.Entities.MopMetadata>()
                        {
                            new BusinessLogic.Entities.MopMetadata()
                            {
                                MetadataKey = new BusinessLogic.Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
                                Value = "true"
                            }
                        }
                    }
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupMopService(_mockMopService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupMopService(_mockMopService);

            // Act
            var result = _viewModelBuilder.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupMopService(_mockMopService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
