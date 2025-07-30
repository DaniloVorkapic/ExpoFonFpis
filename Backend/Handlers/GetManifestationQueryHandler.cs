using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Handlers
{
    public class GetManifestationQueryHandler : BaseQueryHandler<GetFonManifestationQuery, Result<ManifestationDto>, Manifestation>
    {
        public GetManifestationQueryHandler(IGenericRepository<Manifestation> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<ManifestationDto>> Handle(GetFonManifestationQuery request, CancellationToken cancellationToken)
        {
            var manifestation = Repository.GetQueryable()
                .Where(x => x.Id == 1)
                .Include(x => x.ManifestationRegistrations) 
                .AsEnumerable()
                .Select(x => new ManifestationDto(
                    x.Id,
                    x.Name,
                    x.City,
                    x.Venue,
                    (DateTime)x.StartDate!,
                    (DateTime)x.EndDate!,
                    x.AdditionalInformation,
                    (int)x.Capacity,
                    (int)(x.Capacity - (x.ManifestationRegistrations.Where(r => r.IsArtReserved == true).Sum(x => x.NumberOfPeople))),
                    (int)(x.Capacity - (x.ManifestationRegistrations.Where(r => r.IsPhotoReserved == true).Sum(x => x.NumberOfPeople)))
                ))
                .SingleOrDefault();

            return Result<ManifestationDto>.Success(Mapper.Map<ManifestationDto>(manifestation));
        }
    }
}
