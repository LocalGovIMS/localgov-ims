//using BusinessLogic.Enums;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;

//namespace BusinessLogic.UnitTests.Validators.AccountReferenceValidator
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class ValidateReferenceTest : BaseAccountReferenceValidatorTest
//    {
//        [TestMethod]
//        public void ValidateReferenceMaximumFundExceeded()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 10,
//                ValidationReference = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "11"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("123", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeFalse();
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }

//        [TestMethod]
//        public void ValidateReferenceReferenceLengthInvalid()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "11",
//                MaxLength = "2",
//                MinLength = "1"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("100", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeFalse();
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }

//        [TestMethod]
//        public void ValidateReferenceReferenceReturnsInvalidAccount()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12",
//                AccountExist = true
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "11",
//                MaxLength = "200",
//                MinLength = "1"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("100", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeFalse();
//            result.Error.Should().Be("The account reference is not valid");
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }

//        [TestMethod]
//        public void ValidateReferenceReferenceReturnsStoppedAccount()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12",
//                AccountExist = true,
//                FundCode = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "11",
//                MaxLength = "200",
//                MinLength = "1"
//            });

//            MockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>())).Returns(new Entities.AccountHolder()
//            {
//                StopMessageReference = "test stop message"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("100", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeFalse();
//            result.Error.Should().Be("This account has been stopped");
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }

//        [TestMethod]
//        public void ValidateReferenceReferenceReturnsValidAccount()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12",
//                AccountExist = true,
//                FundCode = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "110",
//                MaxLength = "200",
//                MinLength = "1"
//            });

//            MockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>())).Returns(new Entities.AccountHolder()
//            {
//                Forename = "bob",
//                Surname = "jones"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("110", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeTrue();
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }

//        [DataTestMethod]
//        [DataRow("$$", "L1")]
//        [DataRow("##", "1L")]
//        [DataRow("??", "A!")]
//        [DataRow("***", "1 @")]
//        [DataRow("z", "1")]
//        [DataRow("z", "z")]
//        [DataRow("@", "1")]
//        public void ValidateReferenceReferenceReturnsInvalidMask(string inputMask, string reference)
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12",
//                AccountExist = false,
//                FundCode = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = inputMask,
//                MaxLength = "200",
//                MinLength = "1",
//                CheckDigitCalcAlphaReplace = "1",
//                Modulus = "1",
//                AccountValidationWeightings = new List<Entities.AccountValidationWeighting>()
//                {
//                    new Entities.AccountValidationWeighting(){Digit1Weighting ="1",
//                    Digit2Weighting ="2",
//                    Digit3Weighting ="3",
//                    Digit4Weighting ="4",
//                    Digit5Weighting ="5",
//                    Digit6Weighting ="6",
//                    Digit7Weighting ="7",
//                    Digit8Weighting ="8",
//                    Digit9Weighting ="9",
//                    Digit10Weighting ="10",
//                    Digit11Weighting ="11",
//                    Digit12Weighting ="12",
//                    Digit13Weighting ="13",
//                    Digit14Weighting ="14",
//                    Digit15Weighting ="15",
//                    Digit16Weighting ="16",
//                    Digit17Weighting ="17",
//                    Digit18Weighting ="18",
//                    Digit19Weighting ="19",
//                    Digit20Weighting ="20",
//                    Digit21Weighting ="21",
//                    Digit22Weighting ="22",
//                    Digit23Weighting ="23",
//                    Digit24Weighting ="24",
//                    Digit25Weighting ="25",
//                    Digit26Weighting ="26",
//                    Digit27Weighting ="27",
//                    Digit28Weighting ="28",
//                    Digit29Weighting ="29",
//                    Digit30Weighting ="30"
//                    }
//                }
//            });

//            MockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>())).Returns(new Entities.AccountHolder()
//            {
//                Forename = "bob",
//                Surname = "jones"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference(reference, "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeFalse();
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }


//        [TestMethod]
//        public void ValidateReferenceReferenceReturnsValidResponse()
//        {
//            MockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(new Entities.Fund()
//            {
//                MaximumAmount = 999,
//                ValidationReference = "12",
//                AccountExist = false,
//                FundCode = "12"
//            });
//            MockAccountHolderService.Setup(x => x.GetAccountValidation(It.IsAny<string>())).Returns(new Entities.AccountValidation()
//            {
//                InputMask = "1",
//                MaxLength = "200",
//                MinLength = "1"
//            });

//            MockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>())).Returns(new Entities.AccountHolder()
//            {
//                Forename = "bob",
//                Surname = "jones"
//            });

//            var validator = GetAccountReferenceValidator();

//            var result = validator.ValidateReference("1", "123", 100, AccountReferenceValidationSource.Payments);
//            result.Success.Should().BeTrue();
//            result.Should().BeOfType(expectedType: typeof(BusinessLogic.Classes.Result.Result));
//        }
//    }
//}
