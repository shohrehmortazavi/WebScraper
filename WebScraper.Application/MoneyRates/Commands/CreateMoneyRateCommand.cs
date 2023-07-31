using MediatR;
using Microsoft.Extensions.Logging;
using WebScraper.Application.MoneyRates.Dtos;
using WebScraper.Domain.MoneyRates;
using WebScraper.Domain.SeedWorks;

namespace WebScraper.Application.MoneyRates.Commands
{
    public record CreateMoneyRateCommand(MoneyRateDto MoneyRateDto) : IRequest<MoneyRateDto>;

    public class CreateMoneyRateHandler : IRequestHandler<CreateMoneyRateCommand, MoneyRateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateMoneyRateHandler> _logger;

        public CreateMoneyRateHandler(IUnitOfWork unitOfWork,
                                         ILogger<CreateMoneyRateHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<MoneyRateDto> Handle(CreateMoneyRateCommand request, CancellationToken cancellationToken)
        {
            var moneyRate = request.MoneyRateDto;

            if (moneyRate == null)
                _logger.LogCritical("There is no Data as Money Rate!");

            if (string.IsNullOrEmpty(moneyRate.Name))
                _logger.LogError("Name is NULL!");

            if (string.IsNullOrEmpty(moneyRate.Symbol))
                _logger.LogError("Code is NULL!");

            if (Convert.ToDateTime(moneyRate.CurrentDate) > DateTime.Now)
                _logger.LogError("Current Date is greater than Now");

            if (Convert.ToDateTime(moneyRate.CurrentTime) > DateTime.Now)
                _logger.LogError("Current Time is greater than Now");

            var current = Convert.ToDateTime(moneyRate.CurrentDate + " " + moneyRate.CurrentTime);
            var moneyRateDb = new MoneyRate(moneyRate.Name, moneyRate.Symbol, moneyRate.Sell,
                                            moneyRate.Buy, current);

            await _unitOfWork.MoneyRateWriteRepository.CreateAsync(moneyRateDb);
            await _unitOfWork.Commit();

            moneyRate.Id = moneyRateDb.Id;

            return await Task.FromResult(moneyRate);
        }
    }
}
