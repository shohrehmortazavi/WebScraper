using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebScraper.Application.MoneyRates.Queries;

namespace WebScraper.Api.Controllers
{
    public class MoneyRateController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetMoneyRatesQuery());

            if (response == null || !response.Any())
                return NoContent();

            return Ok(response);
        }


    }
}
