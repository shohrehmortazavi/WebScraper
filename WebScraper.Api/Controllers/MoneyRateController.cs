using Microsoft.AspNetCore.Mvc;
using WebScraper.Application.MoneyRates.Dtos;
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

        [HttpPost("GetAverage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAverage(MoneyRateAverageRequestDto moneyRateAverageDto)
        {
            var response = await Mediator.Send(new GetMoneyRateAverageQuery(moneyRateAverageDto));

            if (response == null)
                return NoContent();

            return Ok(response);
        }
    }
}
