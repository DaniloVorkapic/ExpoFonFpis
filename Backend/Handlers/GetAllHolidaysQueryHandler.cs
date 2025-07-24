using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;

namespace Backend.Handlers
{
    public class GetAllHolidaysQueryHandler : BaseQueryHandler<GetAllHolidaysQuery, Result<List<HolidayDto>>, Holiday>
    {
        public GetAllHolidaysQueryHandler(IGenericRepository<Holiday> holidayRepository, IMapper mapper) : base(holidayRepository, mapper)
        {
        }

        public override async Task<Result<List<HolidayDto>>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
        {
            var holidays = await Repository.GetAllAsync();
            return Result<List<HolidayDto>>.Success(Mapper.Map<List<HolidayDto>>(holidays));
        }
    }
}
