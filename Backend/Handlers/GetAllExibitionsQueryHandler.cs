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
    public class GetAllExibitionsQueryHandler : BaseQueryHandler<GetAllExibitionsQuery, Result<List<ExibitionDto>>, Exibition>
    {
        public GetAllExibitionsQueryHandler(IGenericRepository<Exibition> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<ExibitionDto>>> Handle(GetAllExibitionsQuery request, CancellationToken cancellationToken)
        {
            var artExibitions = await Repository.GetQueryable()
                .Include(x => x.Manifestation)
                .Where(x => x.Manifestation.Name == "FonExpo" && (int)x.ExibitionType.Value == request.ExibitionType)
                .Select(x => new ExibitionDto(
                    x.Id,
                    x.Title,
                    x.StartTime.HasValue ? x.StartTime.Value.ToString("HH:mm") : string.Empty,
                    x.EndTime.HasValue ? x.EndTime.Value.ToString("HH:mm") : string.Empty,
                    x.Artist
                    ))
                .ToListAsync(cancellationToken);

            return Result<List<ExibitionDto>>.Success(Mapper.Map<List<ExibitionDto>>(artExibitions));
        }
    }
}
