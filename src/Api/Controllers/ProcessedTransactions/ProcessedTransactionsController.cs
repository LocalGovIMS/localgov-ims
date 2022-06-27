using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers.ProcessedTransactions
{
    public class ProcessedTransactionsController : ApiController
    {
        private readonly ILog _log;
        private readonly IRuleEngine _ruleEngine;
        private readonly ITransactionService _transactionService;

        public ProcessedTransactionsController(
            ILog log,
            IRuleEngine ruleEngine,
            ITransactionService transactionService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _ruleEngine = ruleEngine ?? throw new ArgumentNullException("ruleEngine");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(ProcessedTransactionModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromBody] ProcessedTransactionModel model)
        {
            try
            {
                var transactionToCreate = model.GetProcessedTransaction();

                _ruleEngine.Process(transactionToCreate);

                var result = _transactionService.CreateProcessedTransaction(transactionToCreate);

                if (result.Success)
                {
                    var processedTransactionModel = new ProcessedTransactionModel((ProcessedTransaction)result.Data);

                    return CreatedAtRoute("ProcessedTransactionsGet", new { reference = processedTransactionModel.Reference }, processedTransactionModel);
                }

                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/ProcessedTransactions/{reference}", Name = "ProcessedTransactionsGet")]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(ProcessedTransactionModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult Get(string reference)
        {
            try
            {
                var result = _transactionService.GetTransaction(reference);

                if (result == null)
                    return NotFound();

                return Ok(new ProcessedTransactionModel(result));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(List<ProcessedTransactionModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        [SwaggerOperation(operationId: "ProcessedTransactions_Search")]
        public IHttpActionResult Get([FromUri(Name = "")] SearchCriteriaModel searchCriteriaModel)
        {
            try
            {
                var result = _transactionService.SearchTransactions(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new ProcessedTransactionModel(x)).ToList());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/ProcessedTransactions/{reference}/UpdateCardDetails")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Ok", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        public IHttpActionResult UpdateCardDetails([FromUri] string reference, [FromBody] UpdateCardDetailsModel model)
        {
            try
            {
                var result = _transactionService.UpdateCardDetails(model.ToUpdateCardDetailsArgs());

                if (result.Success)
                    return Ok();

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
