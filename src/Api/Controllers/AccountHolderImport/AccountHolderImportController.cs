using Api.Controllers.ProcessedTransactions;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Web.Http;

namespace Api.Controllers.AccountHolderImport
{
    public class AccountHolderImportController : ApiController
    {
        private readonly ILog _log;
        private readonly IImportProcessor _importProcessor;        

        public AccountHolderImportController(
            ILog log,
            IImportProcessor importProcessor)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _importProcessor = importProcessor ?? throw new ArgumentNullException("importProcessor");
        }

        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, "Created", typeof(AccountHolderImportModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", typeof(string))]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromBody] AccountHolderImportModel model)
        {
            try
            {
                var import = model.GetImport();

                var result = _importProcessor.Process(new ImportProcessorArgs() { Import = import });

                if (result.Success)
                    return Ok(result.Data);

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
