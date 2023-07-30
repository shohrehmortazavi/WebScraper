using MediatR;
using WebScraper.Application.CurrencyRates.Dtos;
using WebScraper.Domain.SeedWorks;

namespace WebScraper.Application.CurrencyRates.Queries
{
    public record GetCurrencyRatesQuery() : IRequest<List<CurrencyRateDto>>;


    public class GetCurrencyRatesHandler : IRequestHandler<GetCurrencyRatesQuery, List<CurrencyRateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCurrencyRatesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CurrencyRateDto>> Handle(GetCurrencyRatesQuery request, CancellationToken cancellationToken)
        {

            var list = await _unitOfWork.CurrencyRateReadRepository.GetAllAsync();

            if (list == null || !list.Any())
                return null;

            return list.Select(x => new CurrencyRateDto
            {
                Id = x.Id,
                Rate = x.Rate,
                Symbol = x.Symbol,
                CurrentTime = x.CurrentTime,
                CurrentDate = x.CurrentDate
            }).ToList();
        }
    }

}
