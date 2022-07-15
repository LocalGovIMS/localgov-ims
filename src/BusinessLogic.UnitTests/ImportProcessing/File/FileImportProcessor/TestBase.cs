using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.ImportProcessing.File.FileImportProcessor
{
    [TestClass]
    public class TestBase
    {
        protected Mock<ILog> MockLogger = new Mock<ILog>();
        protected Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected Mock<IRuleEngine> MockRuleEngine = new Mock<IRuleEngine>();
        protected Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected Mock<IProcessedTransactionModelBuilder> MockProcessedTransactionModelBuilder = new Mock<IProcessedTransactionModelBuilder>();

        protected BusinessLogic.ImportProcessing.FileImportProcessor ImportProcessor;
        protected FileImportProcessorArgs Args;

        protected void SetupFileImportProcessor()
        {
            ImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                MockLogger.Object,
                MockSecurityContext.Object,
                MockUnitOfWork.Object,
                MockRuleEngine.Object,
                MockTransactionService.Object,
                MockProcessedTransactionModelBuilder.Object);
        }

        protected void SetupFileImportProcessorArgs()
        {
            Args = new FileImportProcessorArgs()
            {
                ImportId = 1
            };
        }

        protected Entities.FileImport GetImport()
        {
            return new Entities.FileImport()
            {
                ImportId = 1,
                Rows = new List<Entities.FileImportRow>()
                {
                    new Entities.FileImportRow()
                    {
                        RowData = "gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "e2rzEfBWB,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.9490327,2021-11-22 11:30:41.9490327,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "bmWRSRWxx1,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.9879299,2021-11-22 11:30:41.9879299,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "dPyjsJlmz7,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:42.0298161,2021-11-22 11:30:42.0298161,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "QOK4C9DQGo,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.4851828,2021-11-22 11:31:19.4851828,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "PvQ8cMWjE,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.5300617,2021-11-22 11:31:19.5300617,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "pDZ6CymxaK,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.5679582,2021-11-22 11:31:19.5679582,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "OvVXsjQQ1M,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.6058584,2021-11-22 11:31:19.6058584,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "zE19IBQb57,mjp4U31P2W,mjp4U31P2W,SP,2021-11-24 14:00:58.6373463,2021-11-24 14:00:58.6373463,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "Z6QGun4bEE,mjp4U31P2W,mjp4U31P2W,SP,2021-11-24 14:00:58.6902049,2021-11-24 14:00:58.6902049,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "G2kbT8NPq,mjp4U31P2W,mjp4U31P2W,SP,2021-11-24 14:00:58.7251110,2021-11-24 14:00:58.7251110,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "AqdQCl20D,mjp4U31P2W,mjp4U31P2W,SP,2021-11-24 14:00:58.7690219,2021-11-24 14:00:58.7690219,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "Dmrviyd4JW,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:12.9678353,2021-11-24 14:01:12.9678353,12345678901IM,1,13,11,0.00,W0,0.2,3.33,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "DmD7INpXMl,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:13.0067303,2021-11-24 14:01:13.0067303,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "mjdVTQA98,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:13.0395689,2021-11-24 14:01:13.0395689,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "3DXVcxakOa,lkV9UVQ7nr,lkV9UVQ7nr,SP,2021-11-24 14:01:23.6897813,2021-11-24 14:01:23.6897813,12345678901IM,1,13,11,0.00,W0,0.2,3.33,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "9VKQH2mEnv,lkV9UVQ7nr,lkV9UVQ7nr,SP,2021-11-24 14:01:23.7286767,2021-11-24 14:01:23.7286767,12345678910IM,1,13,10,20.00,W0,0.2,6.67,Transfer"
                    }
                }
            };
        }

        protected Entities.FileImport GetBadImport()
        {
            return new Entities.FileImport()
            {
                ImportId = 1,
                Rows = new List<Entities.FileImportRow>()
                {
                    new Entities.FileImportRow()
                    {
                        RowData = "gaR4jfj2G,OvqPtXJ89,OvqPtXJ89,SP,202gfgfd1-11-22 11:30:41.8941794,2021-11-22 11:30:41.8941794,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "e2rzEfBWB,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11:30:41.9490327,2021-gfdgfd11-22 11:30:41.9490327,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "bmWRSRWxx1,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 gfd11:30:41.9879299,20gfd21-11-22 11:30:41.9879299,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "dPyjsJlmz7,OvqPtXJ89,OvqPtXJ89,SP,2021-11-22 11gfd:30:42.0298161,2021-11-22 11:30:42.0298161,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "QOK4C9DQGo,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.4851828,2021-11-22 11:31:gfd19.4851828,12345678901IM,1,13,11,6.6gfd6,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "PvQ8cMWjE,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.5300617,2021-11-22 11:31:19.5300617,12345678904IM,1,13,10,13.3gfdgfd4,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "pDZ6CymxaK,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.5679582,2021-11-22 11:31:19.56gfd79582,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "OvVXsjQQ1M,np1aUqZ0D,np1aUqZ0D,SP,2021-11-22 11:31:19.6,,,,,058584,2021-11gfd-22 11:31:19.6058584,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "zE19IBQb57,mjp4U31P2W,mjp4U31,,,P2W,SP,2021-11-24 14:00:58.6373463,2021-11-24 14:00:58.6373463,12345678901IM,1,13,11,6.66,W0,0.2,4.44,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "Z6QGun4bEE,mjp4U31P2W,mjp4U31P2W,SP,2021-htrhtsrhrts-24 14:00:58.6902049,2021-11-24 14:00:58.6902049,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "G2kbT8NPq,mjp4U31P2W,mjp4U31P2W,SP,2021-11 fg gfgf-24 14:00:58.7251110,2021-11-24 14:00:58.7251110,12345678902IM,1,13,11,3.34,W0,0.2,3.89,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "AqdQCl20D,mjp4U31P2W,mjp4U31P2W,SP,2021-176547671-24 14:00:58.7690219,2021-11-24 14:00:58.7690219,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "Dmrviyd4JW,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:12.9678353,2021-11-24 14:01:12.976537653765678353,12345678901IM,1,176533,11,0.00,W0,0.2,3.33,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "DmD7INpXMl,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:13.0067303,2021-11-24 14:765376501:13.0067303,12345678904IM,1,13,10,13.34,W0,0.2,5.56,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "mjdVTQA98,9VOrCwaBV,9VOrCwaBV,SP,2021-11-24 14:01:13.0395689,2021-11-24 14:01:13.0395689,12345678905IM,1,13,10,16.66,W0,0.2,6.11,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "3DXVcxakOa,lkV9UVQ7nr,lkV9UVQ7nr,SP,2021-11-24 14:01:23.6897813,2021-11-24 14:01:23.6897813,12345678901IM,1,13,11,0.00,W0,0.2,mnu ke,Transfer"
                    },
                    new Entities.FileImportRow()
                    {
                        RowData = "9VKQH2mEnv,lkV9UVQ7nr,lkV9UVQ7nr,SP,2021-11-24 14:01:23.7286767,2021-11-24 14:01:23.7286767,12345678910IM,1,13,10,2 ytrw00,W0,0.2,6.w yt7,Transfer"
                    }
                }
            };
        }

        protected ProcessedTransactionModel GetProcessedTransactionModel()
        {
            var model = new ProcessedTransactionModel();

            model.ImportId = 1;
            model.Reference = "ABC123";
            model.InternalReference = "DEF456";
            model.PspReference = "GHI789";
            model.OfficeCode = "O1";
            model.EntryDate = System.DateTime.Now;
            model.TransactionDate = System.DateTime.Now;
            model.AccountReference = "AR1";
            model.UserCode = 1;
            model.FundCode = "F1";
            model.MopCode = "M1";
            model.Amount = 10.0M;
            model.VatCode = "V1";
            model.VatRate = 0.25F;
            model.VatAmount = 2.5M;
            model.Narrative = "A narrative";

            return model;
        }
    }
}

