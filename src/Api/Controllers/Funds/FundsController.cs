using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Web.Http;

namespace Api.Controllers.Funds
{
    public class FundsController : ApiController
    {
        private readonly ILog _log;
        private readonly IFundService _fundService;

        public FundsController(
            ILog log,
            IFundService fundService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _fundService = fundService ?? throw new ArgumentNullException("fundService");
        }

        [HttpGet]
        [Route("api/Funds/{fundCode}", Name = "FundsGet")]
        public IHttpActionResult Get(string fundCode)
        {
            try
            {
                var result = _fundService.GetByFundCode(fundCode);

                if (result == null)
                    return NotFound();

                return Ok(new FundModel(result));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
