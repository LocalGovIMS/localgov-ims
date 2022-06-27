using Api.Controllers.ProcessedTransactions;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web.Http;

namespace Api.Controllers.TransactionImport
{
    public class TransactionImportController : ApiController
    {
        private readonly ILog _log;
        private readonly ITransactionImportProcessor _transactionImportProcessor;        

        public TransactionImportController(
            ILog log,
            ITransactionImportProcessor transactionImportProcessor)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _transactionImportProcessor = transactionImportProcessor ?? throw new ArgumentNullException("transactionImportProcessor");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(TransactionImportModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromBody] TransactionImportModel model)
        {
            try
            {
                var transactionImport = model.GetTransactionImport();

                var result = _transactionImportProcessor.Process(new TransactionImportProcessorArgs() { TransactionImport = transactionImport });

                if (result.Success)
                    return Ok(result.Data);

                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
