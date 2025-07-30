using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetCalculatedPriceQueryHandler : BaseQueryHandler<GetCalculatedPriceQuery, Result<CalculatedDto>, Manifestation>
    {
        public GetCalculatedPriceQueryHandler(IGenericRepository<Manifestation> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<CalculatedDto>> Handle(GetCalculatedPriceQuery request, CancellationToken cancellationToken)
        {

            var manifestation = Repository.GetQueryable()
                .AsNoTracking()
                .SingleOrDefault();

            if (manifestation == null)
            {
                return Result<CalculatedDto>.Failure("Manifestation not found");
            }

            decimal priceArt = (decimal)manifestation.BasePriceArt;
            decimal pricePhoto = (decimal)manifestation.BasePricePhoto;
            var dto = request.calculatedDto;

            decimal totalPrice = 0;

            bool reservedBoth = dto.ReserveArt && dto.ReservePhoto;
            if (reservedBoth)
            {
                totalPrice = (priceArt + pricePhoto) * 0.9m;
            }
            else
            {
                totalPrice += dto.ReserveArt ? priceArt : 0m;
                totalPrice += dto.ReservePhoto ? pricePhoto : 0m;
            }

            // Apply group discount
            if (dto.IsGroupRegistration)
            {
                decimal groupDiscountFactor = 1m;

                if (dto.NumberOfPeople >= 5)
                    groupDiscountFactor = 0.95m;
                else if (dto.NumberOfPeople >= 3)
                    groupDiscountFactor = 0.97m;

                totalPrice = (decimal)(totalPrice * dto.NumberOfPeople);
                totalPrice = totalPrice * groupDiscountFactor;
            }

            return Result<CalculatedDto>.Success(Mapper.Map<CalculatedDto>(totalPrice));
        }
    }
}
