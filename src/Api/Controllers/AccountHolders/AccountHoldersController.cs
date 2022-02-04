using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web.Http;

namespace Api.Controllers.AccountHolders
{
    public class AccountHoldersController : ApiController
    {
        private readonly ILog _log;
        private readonly IAccountHolderService _accountHolderService;

        public AccountHoldersController(
            ILog log,
            IAccountHolderService accountHolderService)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _accountHolderService = accountHolderService ?? throw new ArgumentNullException("accountHolderService");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(AccountHolderModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromBody] AccountHolderModel model)
        {
            try
            {
                var result = _accountHolderService.Create(model.GetAccountHolder());

                if (result.Success)
                {
                    var accountHolderModel = new AccountHolderModel((AccountHolder)result.Data);

                    return CreatedAtRoute("AccountHoldersGet", new { reference = accountHolderModel.AccountReference }, accountHolderModel);
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
        [Route("api/AccountHolders/{reference}", Name = "AccountHoldersGet")]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(AccountHolderModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult Get(string reference)
        {
            try
            {
                var result = _accountHolderService.GetByAccountReference(reference);

                if (result == null)
                    return NotFound();

                return Ok(new AccountHolderModel(result));
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpPatch]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(AccountHolderModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        public IHttpActionResult Patch([FromBody] AccountHolderModel model)
        {
            try
            {
                var result = _accountHolderService.Update(model.GetAccountHolder());

                if (result.Success)
                    return Ok(new AccountHolderModel((AccountHolder)result.Data));

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
