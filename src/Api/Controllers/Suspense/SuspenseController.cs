using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;
using System.Web.Http;

namespace Api.Controllers.Suspense
{
    public class SuspenseController : ApiController
    {
        private readonly ILog _log;
        private readonly ISuspenseService _suspenseService;

        public SuspenseController(
            ILog log,
            ISuspenseService suspenseService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _suspenseService = suspenseService ?? throw new ArgumentNullException("suspenseService");
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] SuspenseModel model)
        {
            try
            {
                var result = _suspenseService.Create(model.GetSuspense());

                if (result.Success)
                {
                    var suspenseModel = new SuspenseModel((BusinessLogic.Entities.Suspense)result.Data);

                    return CreatedAtRoute("SuspenseGet", new { id = suspenseModel.Id }, suspenseModel);
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
        [Route("api/Suspense/{id}", Name = "SuspenseGet")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _suspenseService.GetSuspense(id);

                if (result == null)
                    return NotFound();

                return Ok(new SuspenseModel(result.Item));
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
                var result = _suspenseService.Search(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new SuspenseModel(x.Item)));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
