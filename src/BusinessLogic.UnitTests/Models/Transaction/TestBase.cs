using BusinessLogic.Enums;
using System;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    public class TestBase
    {
        protected const string PspReference = "1234567890";
        protected DateTime LatestEntryDate = new DateTime(2021, 10, 4, 14, 55, 00);
        protected DateTime LatestTransactionDate = new DateTime(2021, 10, 4, 14, 55, 00);
        protected DateTime LastUpdatedDate = new DateTime(2021, 10, 5, 14, 55, 00);
        protected const string AuthorisationCode = "AuthCode";
        protected const string MopCode = "MC";
        protected const string MopName = "MopName";
        protected const string CardHolderPremiseNumber = "1";
        protected const string CardHolderStreet = "CardHolderStreet";
        protected const string CardHolderTown = "CardHolderTown";
        protected const string CardHolderPostCode = "CardHolderPostCode";
        protected const string CardHolderName = "CardHolderName";
        protected const string Username = "Username";
        protected const bool ReceiptIssued = true;

        protected BusinessLogic.Models.Transaction GetTransaction()
        {
            return GetTransaction(Username);
        }

        protected BusinessLogic.Models.Transaction GetTransaction(string username)
        {
            return new BusinessLogic.Models.Transaction(
                GetProcessedTransactions(username),
                GetPendingRefunds(),
                GetFailedRefunds(),
                GetProcessedRefunds(),
                GetTransfers(),
                PspReference);
        }

        protected BusinessLogic.Models.Transaction GetCreditNoteTransaction()
        {
            return new BusinessLogic.Models.Transaction(
                GetCreditNoteTransactions(),
                GetPendingRefunds(),
                GetFailedRefunds(),
                GetProcessedRefunds(),
                GetTransfers(),
                PspReference);
        }

        protected BusinessLogic.Models.Transaction GetTransactionsWithoutAUser()
        {
            var processedTransctions = GetProcessedTransactions(Username);

            processedTransctions.ForEach(c => c.User = null);

            return new BusinessLogic.Models.Transaction(
                processedTransctions,
                GetPendingRefunds(),
                GetFailedRefunds(),
                GetProcessedRefunds(),
                GetTransfers(),
                PspReference);
        }

        private List<Entities.ProcessedTransaction> GetProcessedTransactions(string username)
        {
            return new List<Entities.ProcessedTransaction>()
            {
                // This is the transaction mostly used in the Transaction class
                new Entities.ProcessedTransaction()
                {
                    AuthorisationCode = AuthorisationCode,
                    TransactionDate = LatestTransactionDate,
                    PspReference = PspReference,
                    EntryDate = LatestEntryDate,
                    Amount = 10,
                    Mop = new Entities.Mop()
                    {
                        MopName = MopName,
                        MopCode = MopCode
                    },
                    MopCode = MopCode,
                    CardHolderPremiseNumber = CardHolderPremiseNumber,
                    CardHolderStreet = CardHolderStreet,
                    CardHolderTown = CardHolderTown,
                    CardHolderPostCode = CardHolderPostCode,
                    CardHolderName = CardHolderName,
                    User = username == null ? null : new Entities.User()
                    {
                        DisplayName = username
                    },
                    ReceiptIssued = ReceiptIssued
                },
                new Entities.ProcessedTransaction()
                {
                    PspReference = PspReference,
                    Amount = 20,
                    User = new Entities.User()
                    {
                        DisplayName = username
                    }
                },
                new Entities.ProcessedTransaction()
                {
                    PspReference = PspReference,
                    Amount = 30,
                    User = new Entities.User()
                    {
                        DisplayName = username
                    }
                }
            };
        }

        private List<Entities.PendingTransaction> GetPendingRefunds()
        {
            return new List<Entities.PendingTransaction>();
        }

        private List<Entities.PendingTransaction> GetFailedRefunds()
        {
            return new List<Entities.PendingTransaction>();
        }

        private List<Entities.ProcessedTransaction> GetProcessedRefunds()
        {
            return new List<Entities.ProcessedTransaction>()
            {
                new Entities.ProcessedTransaction()
                {
                    TransactionDate = LastUpdatedDate
                }
            };
        }

        private List<Entities.ProcessedTransaction> GetTransfers()
        {
            return new List<Entities.ProcessedTransaction>();
        }

        private List<Entities.ProcessedTransaction> GetCreditNoteTransactions()
        {
            return new List<Entities.ProcessedTransaction>()
            {
                new Entities.ProcessedTransaction()
                {
                    AuthorisationCode = AuthorisationCode,
                    TransactionDate = LatestTransactionDate,
                    PspReference = PspReference,
                    EntryDate = LatestEntryDate,
                    Amount = -10,
                    Mop = new Entities.Mop()
                    {
                        MopName = MopName,
                        MopCode = MopCode,
                        Metadata = new List<Entities.MopMetadata>()
                        {
                            new Entities.MopMetadata()
                            {
                                Key = MopMetadataKeys.IsAJournalReallocation,
                                Value = "True"
                            }
                        }
                    },
                    MopCode = MopCode,
                    CardHolderPremiseNumber = CardHolderPremiseNumber,
                    CardHolderStreet = CardHolderStreet,
                    CardHolderTown = CardHolderTown,
                    CardHolderPostCode = CardHolderPostCode,
                    CardHolderName = CardHolderName,
                    User = new Entities.User()
                    {
                        DisplayName = "Username"
                    }
                },
                new Entities.ProcessedTransaction()
                {
                    PspReference = PspReference,
                    Amount = -20,
                    Mop = new Entities.Mop()
                    {
                        MopName = MopName,
                        MopCode = MopCode,
                        Metadata = new List<Entities.MopMetadata>()
                        {
                            new Entities.MopMetadata()
                            {
                                Key = MopMetadataKeys.IsAJournalReallocation,
                                Value = "True"
                            }
                        }
                    },
                    User = new Entities.User()
                    {
                        DisplayName = "Username"
                    }
                },
                new Entities.ProcessedTransaction()
                {
                    PspReference = PspReference,
                    Amount = -30,
                    Mop = new Entities.Mop()
                    {
                        MopName = MopName,
                        MopCode = MopCode,
                        Metadata = new List<Entities.MopMetadata>()
                        {
                            new Entities.MopMetadata()
                            {
                                Key = MopMetadataKeys.IsAJournalReallocation,
                                Value = "True"
                            }
                        }
                    },
                    User = new Entities.User()
                    {
                        DisplayName = "Username"
                    }
                }
            };
        }
    }
}
