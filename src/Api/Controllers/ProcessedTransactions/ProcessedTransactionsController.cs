using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
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
        public IHttpActionResult Get([FromUri] SearchCriteriaModel searchCriteriaModel)
        {
            try
            {
                var result = _transactionService.SearchTransactions(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new ProcessedTransactionModel(x)));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
