using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.EReturn;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.EReturns.Add(It.IsAny<Type>()));

            MockUnitOfWork.Setup(x => x.Templates.GetTemplate(It.IsAny<int>()))
                .Returns(new Entities.Template()
                {
                    Id = 1,
                    TemplateRows = new List<Entities.TemplateRow>()
                    {
                        new Entities.TemplateRow()
                        {
                            Id = 1,
                            Reference = "Test",
                            VatCode = "V1",
                            Description = "Test",
                            VAT = new Entities.Vat()
                            {
                                Percentage = 20
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Funds.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Fund>() {
                    new Entities.Fund()
                    {
                        FundCode = "01",
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                Key = FundMetadataKeys.IsAnEReturnDefaultFund,
                                Value = "True"
                            }
                        }
                    },
                    new Entities.Fund()
                    {
                        FundCode = "13",
                        Metadata = new List<Entities.FundMetadata>()
                        {
                            new Entities.FundMetadata()
                            {
                                Key = FundMetadataKeys.IsAnEReturnDefaultFund,
                                Value = "True"
                            }
                        }
                    }
                });

            MockUnitOfWork.Setup(x => x.Mops.GetAll(It.IsAny<bool>()))
                .Returns(new List<Entities.Mop>() {
                    new Entities.Mop()
                    {
                        MopCode = "01",
                        Metadata = new List<Entities.MopMetadata>()
                        {
                            new Entities.MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsACashPayment
                                },
                                Value = "True"
                            }
                        }
                    },
                    new Entities.Mop()
                    {
                        MopCode = "02",
                        Metadata = new List<Entities.MopMetadata>()
                        {
                            new Entities.MopMetadata()
                            {
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAnEReturnChequePayment
                                },
                                Value = "True"
                            }
                        }
                    }
                });
        }

        private void SetupTemplateService()
        {
            MockTemplateService.Setup(x => x.GetTemplate(It.IsAny<int>()))
                .Returns(new Entities.Template()
                {
                    Id = 1,
                    TemplateRows = new List<Entities.TemplateRow>()
                    {
                        new Entities.TemplateRow()
                        {
                            Id = 1,
                            Reference = "Test",
                            VatCode = "V1",
                            Description = "Test",
                            VAT = new Entities.Vat()
                            {
                                Percentage = 20
                            }
                        }
                    }
                });
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        private Type GetEReturn(EReturnType type)
        {
            return new Type()
            {
                Id = 1,
                TypeId = (int)type
            };
        }

        [DataRow(EReturnType.Cash)]
        [DataRow(EReturnType.Cheque)]
        [TestMethod]
        public void ReturnsCorrectType(EReturnType type)
        {
            // Arrange
            SetupUnitOfWork();
            SetupTemplateService();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.Create(GetEReturn(type));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void OnErrorReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(true);
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Create(GetEReturn(EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void IncorrectSecurityReturnsFailure()
        {
            // Arrange
            SetupSecurityContext(false);
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Create(GetEReturn(EReturnType.Cash));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.EReturns };
            SetupUnitOfWork();
            SetupTemplateService();
            var service = GetService();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.Create(GetEReturn(EReturnType.Cash));

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.Create(GetEReturn(EReturnType.Cash));

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Result));
                    Assert.IsFalse(result.Success);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
