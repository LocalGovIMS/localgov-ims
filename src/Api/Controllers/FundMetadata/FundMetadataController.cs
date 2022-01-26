using BusinessLogic.Interfaces.Services;
using log4net;
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
        [Route("api/FundMetadata/{fundCode}/{key}", Name = "FundMetadataGet")]
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
