﻿using Microsoft.AspNetCore.Mvc;
using WebScraper.Application.CurrencyRates.Queries;

namespace WebScraper.Api.Controllers
{
    public class CurrencyRateController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetCurrencyRatesQuery());

            if (response == null || !response.Any())
                return NoContent();

            return Ok(response);
        }

    }
}
