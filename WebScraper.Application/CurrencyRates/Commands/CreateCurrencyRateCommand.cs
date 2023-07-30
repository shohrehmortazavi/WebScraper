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

            if (currencyRate == null)
                _logger.LogCritical("There is no Data as Currency Rate!");

            if (string.IsNullOrEmpty(currencyRate.Symbol))
                _logger.LogError("Symbol is NULL!");

            if (currencyRate.CurrentDate > DateOnly.FromDateTime(DateTime.Now))
                _logger.LogError("Current Date is greater than Now");

            if (currencyRate.CurrentTime > TimeOnly.FromDateTime(DateTime.Now))
                _logger.LogError("Current Time is greater than Now");


            var currencyRateDb = new CurrencyRate(currencyRate.Symbol, currencyRate.Rate, currencyRate.CurrentTime, currencyRate.CurrentDate);

            await _unitOfWork.CurrencyRateWriteRepository.CreateAsync(currencyRateDb);
            await _unitOfWork.Commit();

            currencyRate.Id = currencyRateDb.Id;

            return await Task.FromResult(currencyRate);
        }
    }
}
