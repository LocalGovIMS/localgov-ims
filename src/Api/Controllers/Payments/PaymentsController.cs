using Api.Controllers.PendingTransactions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web.Http;

namespace Api.Controllers.AccountHolders
{
    public class PaymentsController : ApiController
    {
        private readonly ILog _log;
        private readonly IPaymentService _paymentService;

        public PaymentsController(
            ILog log,
            IPaymentService paymentService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _paymentService = paymentService ?? throw new ArgumentNullException("paymentService");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(PaymentResponseModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromBody] PaymentModel model)
        {
            try
            {
                var result = _paymentService.CreateHppPayments(model.GetPaymentDetails());

                var response = new PaymentResponseModel(result);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
