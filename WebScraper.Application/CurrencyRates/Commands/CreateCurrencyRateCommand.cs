using MediatR;
using Microsoft.Extensions.Logging;
using WebScraper.Application.CurrencyRates.Dtos;
using WebScraper.Domain.CurrencyRates;
using WebScraper.Domain.SeedWorks;

namespace WebScraper.Application.CurrencyRates.Commands
{
    public record CreateCurrencyRateCommand(CurrencyRateDto CurrencyRateDto) : IRequest<CurrencyRateDto>;

    public class CreateCurrencyRateHandler : IRequestHandler<CreateCurrencyRateCommand, CurrencyRateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCurrencyRateHandler> _logger;

        public CreateCurrencyRateHandler(IUnitOfWork unitOfWork,
                                         ILogger<CreateCurrencyRateHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CurrencyRateDto> Handle(CreateCurrencyRateCommand request, CancellationToken cancellationToken)
        {
            var currencyRate = request.CurrencyRateDto;
            var current = Convert.ToDateTime(currencyRate.CurrentDate + " " + currencyRate.CurrentTime);

            if (currencyRate == null)
                _logger.LogCritical("There is no Data as Currency Rate!");

            if (string.IsNullOrEmpty(currencyRate.Symbol))
                _logger.LogError("Symbol is NULL!");

            if (current > DateTime.Now)
                _logger.LogError("Current Date and Time is greater than Now");



            var currencyRateDb = new CurrencyRate(currencyRate.Symbol, currencyRate.Rate, current);

            await _unitOfWork.CurrencyRateWriteRepository.CreateAsync(currencyRateDb);
            await _unitOfWork.Commit();

            currencyRate.Id = currencyRateDb.Id;

            return await Task.FromResult(currencyRate);
        }
    }
}
