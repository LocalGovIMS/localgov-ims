using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers.MethodOfPayments
{
    public class MethodOfPaymentsController : ApiController
    {
        private readonly ILog _log;
        private readonly IMethodOfPaymentService _methodOfPaymentService;

        public MethodOfPaymentsController(
            ILog log,
            IMethodOfPaymentService methodOfPaymentService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _methodOfPaymentService = methodOfPaymentService ?? throw new ArgumentNullException("methodOfPaymentService");
        }

        [HttpGet]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(List<MethodOfPaymentModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        [SwaggerOperation(operationId: "MethodOfPayments_Search")]
        public IHttpActionResult Get([FromUri] SearchCriteriaModel searchCriteriaModel)
        {
            try
            {
                var result = _methodOfPaymentService.Search(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new MethodOfPaymentModel(x)).ToList());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
