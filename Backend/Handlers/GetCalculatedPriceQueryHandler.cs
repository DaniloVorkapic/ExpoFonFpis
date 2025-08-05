using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Enums;
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
            var hasPromoCode = false;

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

            decimal finalPrice = 0;

            var totalPrice = (dto.ReserveArt ? priceArt : 0m) + (dto.ReservePhoto ? pricePhoto : 0m);

            bool reservedBoth = dto.ReserveArt && dto.ReservePhoto;
            if (reservedBoth)
            {
                finalPrice = (priceArt + pricePhoto) * 0.9m;
            }
            else
            {
                finalPrice += dto.ReserveArt ? priceArt : 0m;
                finalPrice += dto.ReservePhoto ? pricePhoto : 0m;
            }

            var priceAfterDiscountOnDays = finalPrice;

            // Apply group discount
            if (dto.IsGroupRegistration)
            {
                decimal groupDiscountFactor = 1m;

                if (dto.NumberOfPeople >= 5)
                    groupDiscountFactor = 0.95m;
                else if (dto.NumberOfPeople >= 3)
                    groupDiscountFactor = 0.97m;

                finalPrice = (decimal)(finalPrice * dto.NumberOfPeople);
                finalPrice = finalPrice * groupDiscountFactor;
            }

            var priceAfterDiscountOnGroup = finalPrice;

            if (dto.HasPromoCode)
            {
                finalPrice *= 0.95m;
                hasPromoCode = true;
            }
            else if (!string.IsNullOrWhiteSpace(dto.PromoCode))
            {
                var validPromoCodes = Repository.GetQueryable()
                    .AsNoTracking()
                    .Include(x => x.PromoCodes)
                    .SelectMany(x => x.PromoCodes)
                    .Where(x => x.LifecycleStatus.Value == (int)LifeCycleStatusEnum.Active)
                    .Select(x => x.Code)
                    .ToList();

                if (validPromoCodes.Contains(dto.PromoCode))
                {
                    finalPrice *= 0.95m;
                    hasPromoCode = true;
                }
            }

            var priceAfterPromoCodeDiscount = finalPrice;

            var calculatedDto = new CalculatedDto(totalPrice, priceAfterDiscountOnDays, priceAfterDiscountOnGroup, priceAfterPromoCodeDiscount, finalPrice, hasPromoCode);

            return Result<CalculatedDto>.Success(Mapper.Map<CalculatedDto>(calculatedDto));
        }
    }
}
