using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Backend.Handlers
{
    public class GetAllRegistrationsQueryHandler : BaseQueryHandler<GetAllRegistrationsQuery, Result<List<RegistrationDto>>, ManifestationRegistration>
    {
        public GetAllRegistrationsQueryHandler(IGenericRepository<ManifestationRegistration> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<RegistrationDto>>> Handle(GetAllRegistrationsQuery request, CancellationToken cancellationToken)
        {
           var registrations = await Repository.GetQueryable()
                .AsNoTracking()
                .Include(x => x.Manifestation)
                .Include(x => x.LifecycleStatus)
                .Where(x => x.Manifestation.Name == "FonExpo")
                .OrderBy(x => x.LifecycleStatus.Value)
                .Select(x => new RegistrationDto(
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.EmailAddres,
                    x.Price,
                    x.LifecycleStatus.Value,
                    x.LifecycleStatus.EnumValue.ToString()
                    ))
                .ToListAsync(cancellationToken);

            return Result<List<RegistrationDto>>.Success(Mapper.Map<List<RegistrationDto>>(registrations));
        }
    }
}
