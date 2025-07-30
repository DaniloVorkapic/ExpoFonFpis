using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;

namespace Backend.Handlers
{
    public class GetPricesQueryHandler : BaseQueryHandler<GetPricesQuery, Result<PricesDto>, Manifestation>
    {
        public GetPricesQueryHandler(IGenericRepository<Manifestation> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<PricesDto>> Handle(GetPricesQuery request, CancellationToken cancellationToken)
        {
            var prices = Repository.GetQueryable()
                    .AsEnumerable()
                    .Select(x => new PricesDto(
                        x.Id,
                        x.BasePriceArt,
                        x.BasePricePhoto
                    ))
                    .SingleOrDefault();

            return Result<PricesDto>.Success(Mapper.Map<PricesDto>(prices));
        }
    }
}
