using MediatR;
using WebScraper.Application.MoneyRates.Dtos;
using WebScraper.Domain.SeedWorks;

namespace WebScraper.Application.MoneyRates.Queries
{
    public record GetMoneyRatesQuery() : IRequest<List<MoneyRateDto>>;


    public class GetMoneyRatesHandler : IRequestHandler<GetMoneyRatesQuery, List<MoneyRateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMoneyRatesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MoneyRateDto>> Handle(GetMoneyRatesQuery request, CancellationToken cancellationToken)
        {

            var list = await _unitOfWork.MoneyRateReadRepository.GetAllAsync();

            if (list == null || !list.Any())
                return null;

            return list.Select(x => new MoneyRateDto
            {
                Id = x.Id,
                Name = x.Name,
                Buy = x.Buy,
                Sell = x.Sell,
                Symbol = x.Symbol,
                CurrentTime = x.CurrentDate.ToShortTimeString(),
                CurrentDate = x.CurrentDate.ToShortDateString()
            }).ToList();
        }
    }

}
