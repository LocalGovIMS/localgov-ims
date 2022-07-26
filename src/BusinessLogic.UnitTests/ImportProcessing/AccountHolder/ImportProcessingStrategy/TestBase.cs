using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Services;
using Moq;
using Strategy = BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    public class TestBase
    {
        protected readonly Mock<IAccountHolderService> MockAccountHolderService = new Mock<IAccountHolderService>();

        protected Strategy Strategy;

        protected void SetupDependenciesForExistingAccountHolder()
        {
            MockAccountHolderService.Setup(x => x.Update(It.IsAny<UpdateAccountHolderArgs>()))
                .Returns(new Result());

            MockAccountHolderService.Setup(x => x.Create(It.IsAny<CreateAccountHolderArgs>()))
                .Returns(new Result());
        }

        protected void SetupDependenciesForNewAccountHolder()
        {
            MockAccountHolderService.Setup(x => x.Update(It.IsAny<UpdateAccountHolderArgs>()))
                .Returns(new Result("Unable to find the Account Holder record to update"));

            MockAccountHolderService.Setup(x => x.Create(It.IsAny<CreateAccountHolderArgs>()))
                .Returns(new Result());
        }

        protected void SetupDependenciesForAccountHolderCreationFailure(string errorMessage)
        {
            MockAccountHolderService.Setup(x => x.Update(It.IsAny<UpdateAccountHolderArgs>()))
                .Returns(new Result("Unable to find the Account Holder record to update"));

            MockAccountHolderService.Setup(x => x.Create(It.IsAny<CreateAccountHolderArgs>()))
                .Returns(new Result(errorMessage));
        }

        protected void SetupStrategy()
        {
            Strategy = new Strategy(MockAccountHolderService.Object);
        }

        protected ImportProcessingStrategyArgs GetArgs()
        {
            return new ImportProcessingStrategyArgs()
            {
                Import = new Import()
                {
                    Id = 1,
                    CreatedByUserId = 1,
                    CreatedDate = System.DateTime.Now,
                    ImportTypeId = 1,
                    Notes = "Notes",
                    NumberOfRows = 1
                },
                Row = new ImportRow()
                {
                    Id = 1,
                    ImportId = 1,
                    Data = Newtonsoft.Json.JsonConvert.SerializeObject(new Entities.AccountHolder())
                }
            };
        }

    }
}
