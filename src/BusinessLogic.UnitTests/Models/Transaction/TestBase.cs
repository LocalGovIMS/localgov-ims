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
        protected const string CardHolderName = "CardHolderName";
        protected const string CardHolderAddressLine1 = "CardHolderAddressLine1";
        protected const string CardHolderAddressLine2 = "CardHolderAddressLine2";
        protected const string CardHolderAddressLine3 = "CardHolderAddressLine3";
        protected const string CardHolderAddressLine4 = "CardHolderAddressLine4";
        protected const string CardHolderPostCode = "CardHolderPostCode";
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
                    CardHolderName = CardHolderName,
                    CardHolderAddressLine1 = CardHolderAddressLine1,
                    CardHolderAddressLine2 = CardHolderAddressLine2,
                    CardHolderAddressLine3 = CardHolderAddressLine3,
                    CardHolderAddressLine4 = CardHolderAddressLine4,
                    CardHolderPostCode = CardHolderPostCode,
                    
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
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
                                Value = "True"
                            }
                        }
                    },
                    MopCode = MopCode,
                    CardHolderName = CardHolderName,
                    CardHolderAddressLine1 = CardHolderAddressLine1,
                    CardHolderAddressLine2 = CardHolderAddressLine2,
                    CardHolderAddressLine3 = CardHolderAddressLine3,
                    CardHolderAddressLine4 = CardHolderAddressLine4,
                    CardHolderPostCode = CardHolderPostCode,
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
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
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
                                MetadataKey = new Entities.MetadataKey()
                                {
                                    Name = MopMetadataKeys.IsAJournalReallocation
                                },
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
