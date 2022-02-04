using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web.Http;

namespace Api.Controllers.Funds
{
    public class FundMetadataController : ApiController
    {
        private readonly ILog _log;
        private readonly IFundMetadataService _fundMetadataService;

        public FundMetadataController(
            ILog log,
            IFundMetadataService fundMetadataService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _fundMetadataService = fundMetadataService ?? throw new ArgumentNullException("fundMetadataService");
        }

        [HttpGet]
        [Route("api/FundMetadata/{key}/{fundCode}/", Name = "FundMetadataGet")]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(string))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult Get(string fundCode, string key)
        {
            try
            {
                var result = _fundMetadataService.Get(fundCode, key);

                if (result == null)
                    return NotFound();

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
