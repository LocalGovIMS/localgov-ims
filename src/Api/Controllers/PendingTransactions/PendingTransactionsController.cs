using BusinessLogic.Classes;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
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
        public IHttpActionResult Post([FromBody] PendingTransactionModel model)
        {
            try
            {
                // TODO: It would be nice if this returned the transactions that were created.
                // We wouldn't have to go and retrieve the data later on then.
                var response = _transactionService.SavePendingTransaction(model.GetPendingTransaction(), "Api"); // TODO: Workout what the source should be

                if (response.Success)
                {
                    var data = _transactionService.GetPendingTransactionsByInternalReference(response.PaymentId);

                    return CreatedAtRoute("PendingTransactionsGet", new { reference = response.PaymentId }, data.Select(x => new PendingTransactionModel(x)));
                }

                return BadRequest(response.ErrorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/PendingTransactions/{reference}", Name = "PendingTransactionsGet")]
        public IHttpActionResult Get(string reference)
        {
            try
            {
                var result = _transactionService.GetPendingTransactionsByInternalReference(reference);

                if (result.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Select(x => new PendingTransactionModel(x)));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/PendingTransaction/{reference}/Authorise")]
        public IHttpActionResult Authorise([FromUri] string reference, [FromBody] AuthoriseModel model)
        {
            try
            {
                var response = _transactionService.AuthorisePendingTransactionByInternalReference(reference, model.PspReference);

                if (response.Success)
                    return Ok();

                return BadRequest(response.ErrorMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/PendingTransaction/{reference}/ProcessPayment")]
        public IHttpActionResult ProcessPayment([FromUri] string reference, [FromBody] ProcessPaymentModel model)
        {
            try
            {
                var paymentResult = new PaymentResult
                {
                    MerchantReference = model.MerchantReference,
                    AuthResult = model.AuthResult,
                    PaymentMethod = model.PaymentMethod,
                    PspReference = model.PspReference
                };

                var response = _paymentService.ProcessPayment(paymentResult);

                if (response.Success)
                    return Ok(response);

                return BadRequest();
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
