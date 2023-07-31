using MediatR;
using WebScraper.Application.MoneyRates.Dtos;
using WebScraper.Domain.SeedWorks;
using WebScraper.Domain.Share.Enums;

namespace WebScraper.Application.MoneyRates.Queries
{
    public record GetMoneyRateAverageQuery(MoneyRateAverageRequestDto MoneyRateAverage) : IRequest<List<MoneyRateAverageResponseDto>>;


    public class GetMoneyRateAverageHandler : IRequestHandler<GetMoneyRateAverageQuery, List<MoneyRateAverageResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMoneyRateAverageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MoneyRateAverageResponseDto>> Handle(GetMoneyRateAverageQuery request, CancellationToken cancellationToken)
        {
            var moneyRateAvrage = request.MoneyRateAverage;
            decimal average = 0;

            var list = await _unitOfWork.MoneyRateReadRepository
                .GetAllAsync(x => x.CurrentDate > moneyRateAvrage.FromDate
                             && x.CurrentDate <= moneyRateAvrage.ToDate);

            if (list == null || !list.Any())
                return null;

            var moneyRateAverageList = new List<MoneyRateAverageResponseDto>();

            var groupByList = list.GroupBy(x => x.Name);
            foreach (var item in groupByList)
            {
                switch (moneyRateAvrage.MoneyRateType)
                {
                    case MoneyRateTypeEnum.Sell:
                        average = item.Sum(x => x.Sell) / item.Count();
                        break;

                    case MoneyRateTypeEnum.Buy:
                        average = item.Sum(x => x.Buy) / item.Count();
                        break;

                    default:
                        break;
                }


                moneyRateAverageList.Add(new MoneyRateAverageResponseDto {
               Name = item.Key,
               AverageRate = average,
               FromDate=moneyRateAvrage.FromDate.ToShortDateString(),
               ToDate=moneyRateAvrage.ToDate.ToShortDateString(),
               MoneyRateType=moneyRateAvrage.MoneyRateType.ToString(),
                
                });
            }

            return moneyRateAverageList;
        }
    }

}
