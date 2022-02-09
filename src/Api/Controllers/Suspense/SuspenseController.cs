using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
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
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(SuspenseModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
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
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(SuspenseModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
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
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(List<SuspenseModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        [SwaggerOperation(operationId: "Suspense_Search")]
        public IHttpActionResult Get([FromUri] SearchCriteriaModel searchCriteriaModel)
        {
            try
            {
                var result = _suspenseService.Search(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new SuspenseModel(x.Item)).ToList());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }
    }
}
