﻿using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(List<FundModel>))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
        [SwaggerOperation(operationId: "Funds_Search")]
        public IHttpActionResult Get([FromUri] SearchCriteriaModel searchCriteriaModel)
        {
            try
            {
                var result = _fundService.Search(searchCriteriaModel.ToSearchCriteria());

                if (result.Items.IsNullOrEmpty())
                    return NotFound();

                return Ok(result.Items.Select(x => new FundModel(x)).ToList());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Funds/{fundCode}", Name = "FundsGet")]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Not found", null)]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "OK", typeof(FundModel))]
        [SwaggerResponse(System.Net.HttpStatusCode.BadRequest, "Bad request", null)]
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
