using Admin.Classes.ViewModelBuilders.AccountHolder;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using ViewModel = Admin.Models.AccountHolder.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountHolder.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private readonly BusinessLogic.Entities.AccountHolder _accountHolder = new BusinessLogic.Entities.AccountHolder()
            {
                AccountReference = "123",
                AddressLine1 = string.Empty,
                AddressLine2 = string.Empty,
                AddressLine3 = string.Empty,
                AddressLine4 = string.Empty,
                CurrentBalance = 0,
                Forename = string.Empty,
                Fund = new BusinessLogic.Entities.Fund()
                {
                    FundName = "Fund Name"
                },
                FundCode = string.Empty,
                CreatedAt = new DateTime(2022, 7, 26),
                CreatedByUserId = 0,
                CreatedByUser = new BusinessLogic.Entities.User()
                {
                    UserName = "Created By User"
                },
                UpdatedAt = new DateTime(2022, 7, 27),
                UpdatedByUserId = 1,
                UpdatedByUser = new BusinessLogic.Entities.User()
                {
                    UserName = "Updated By User"
                },
                PeriodCredit = 0,
                PeriodDebit = 0,
                Postcode = string.Empty,
                RecordType = string.Empty,
                FundMessageId = null,
                FundMessage = new BusinessLogic.Entities.FundMessage()
                {
                    Message = "A message"
                },
                Surname = string.Empty,
                Title = string.Empty,
                UserField1 = string.Empty,
                UserField2 = string.Empty,
                UserField3 = string.Empty
            };

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountHolderService.Object);
        }

        private void SetupAccountHolderService(Mock<IAccountHolderService> service)
        {
            service.Setup(x => x.GetByAccountReference(It.IsAny<string>()))
                .Returns(_accountHolder);
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);

            // Act
            var result = _viewModelBuilder.Build(new DetailsViewModelBuilderArgs() { AccountReference = "123" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildSetupsViewModelPropertiesAsExpected()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);

            // Act
            var result = _viewModelBuilder.Build(new DetailsViewModelBuilderArgs() { AccountReference = "123" });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewModel));
            Assert.AreEqual(result.AccountReference, _accountHolder.AccountReference);
            Assert.AreEqual(result.Address, _accountHolder.Address());
            Assert.AreEqual(result.AddressLine1, _accountHolder.AddressLine1);
            Assert.AreEqual(result.AddressLine2, _accountHolder.AddressLine2);
            Assert.AreEqual(result.AddressLine3, _accountHolder.AddressLine3);
            Assert.AreEqual(result.AddressLine4, _accountHolder.AddressLine4);
            Assert.AreEqual(result.Postcode, _accountHolder.Postcode);
            Assert.AreEqual(result.CurrentBalance, _accountHolder.CurrentBalance);
            Assert.AreEqual(result.Forename, _accountHolder.Forename);
            Assert.AreEqual(result.FullNameAndTitle, _accountHolder.FullNameAndTitle());
            Assert.AreEqual(result.FundCode, _accountHolder.FundCode);
            Assert.AreEqual(result.FundName, _accountHolder.Fund.FundName);
            Assert.AreEqual(result.CreatedAt, _accountHolder.CreatedAt);
            Assert.AreEqual(result.CreatedByUsername, _accountHolder.CreatedByUser.UserName);
            Assert.AreEqual(result.UpdatedAt, _accountHolder.UpdatedAt);
            Assert.AreEqual(result.UpdatedByUsername, _accountHolder.UpdatedByUser.UserName);
            Assert.AreEqual(result.PeriodCredit, _accountHolder.PeriodCredit);
            Assert.AreEqual(result.PeriodDebit, _accountHolder.PeriodDebit);
            Assert.AreEqual(result.RecordType, _accountHolder.RecordType);
            Assert.AreEqual(result.ShowSelect, false);
            Assert.AreEqual(result.FundMessage, _accountHolder.FundMessage.Message);
            Assert.AreEqual(result.FundMessageId, _accountHolder.FundMessageId);
            Assert.AreEqual(result.Surname, _accountHolder.Surname);
            Assert.AreEqual(result.Title, _accountHolder.Title);
            Assert.AreEqual(result.UserField1, _accountHolder.UserField1);
            Assert.AreEqual(result.UserField2, _accountHolder.UserField2);
            Assert.AreEqual(result.UserField3, _accountHolder.UserField3);

        }

        [TestMethod]
        public void OnBuildReturnsNull()
        {
            // Arrange
            SetupAccountHolderService(_mockAccountHolderService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            Assert.IsNull(result);
        }
    }
}
