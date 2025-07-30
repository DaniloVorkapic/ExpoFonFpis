using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetRegistrationByIdQueryHandler : BaseQueryHandler<GetRegistrationByIdQuery, Result<RegistrationByIdDto>, ManifestationRegistration>
    {
        public GetRegistrationByIdQueryHandler(IGenericRepository<ManifestationRegistration> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<RegistrationByIdDto>> Handle(GetRegistrationByIdQuery request, CancellationToken cancellationToken)
        {
            var registration = Repository.GetQueryable()
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new RegistrationByIdDto(
                    x.FirstName,
                    x.LastName,
                    x.Occupation,
                    x.Address.StreetOne,
                    x.Address.StreetTwo,
                    x.Address.PostCode,
                    x.Address.CityName,
                    x.Address.Country,
                    x.EmailAddres,
                    x.IsGroupRegistration ? "group" : "individual",
                    (int)x.NumberOfPeople,
                    x.IsArtReserved,
                    x.IsPhotoReserved,
                    (decimal)x.Price
                ))
                .SingleOrDefault();

            return Result<RegistrationByIdDto>.Success(Mapper.Map<RegistrationByIdDto>(registration));
        }
    }
}
