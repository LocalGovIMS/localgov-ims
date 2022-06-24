using BusinessLogic.Classes;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers.PendingTransactions
{
    public class PendingTransactionsController : ApiController
    {
        private readonly ILog _log;
        private readonly ITransactionService _transactionService;
        private readonly IPaymentService _paymentService;

        public PendingTransactionsController(
            ILog log,
            ITransactionService transactionService,
            IPaymentService paymentService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _transactionService = transactionService ?? throw new ArgumentNullException("transactionService");
            _paymentService = paymentService ?? throw new ArgumentNullException("paymentService");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(List<PendingTransactionModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults] 
        public IHttpActionResult Post([FromBody] PendingTransactionModel model)
        {
            try
            {
                // TODO: It would be nice if this returned the transactions that were created.
                // We wouldn't have to go and retrieve the data later on then.
                var result = _transactionService.SavePendingTransaction(model.GetPendingTransaction(), "Api"); // TODO: Workout what the source should be

                if (result.Success)
                {
                    var data = _transactionService.GetPendingTransactionsByInternalReference(result.PaymentId);

                    return CreatedAtRoute("PendingTransactionsGet", new { reference = result.PaymentId }, data.Select(x => new PendingTransactionModel(x)).ToList());
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/PendingTransactions/{reference}", Name = "PendingTransactionsGet")]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(List<PendingTransactionModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult Get(string reference)
        {
            try
            {
                var result = _transactionService.GetPendingTransactionsByInternalReference(reference);

                if (result.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Select(x => new PendingTransactionModel(x)).ToList());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/PendingTransaction/{reference}/Authorise")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Ok", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        public IHttpActionResult Authorise([FromUri] string reference, [FromBody] AuthoriseModel model)
        {
            try
            {
                var result = _transactionService.AuthorisePendingTransactionByInternalReference(new BusinessLogic.Models.Transactions.AuthorisePendingTransactionByInternalReferenceArgs()
                {
                    InternalReference = reference,
                    PspReference = model.PspReference
                });

                if (result.Success)
                    return Ok();

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/PendingTransaction/{reference}/ProcessPayment")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Ok", typeof(ProcessPaymentResponse))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult ProcessPayment([FromUri] string reference, [FromBody] ProcessPaymentModel model)
        {
            try
            {
                var result = _paymentService.ProcessPayment(model.ToPaymentResult());

                if (result.Success)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/ProcessedTransactions/{reference}/ProcessFee")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Ok", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult ProcessFee([FromUri] string reference, [FromBody] ProcessFeeModel model)
        {
            try
            {
                var result = _paymentService.ProcessFee(model.ToPaymentResult());

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
