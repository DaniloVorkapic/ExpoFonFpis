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
    public class GetConfirmationQueryHandler : BaseQueryHandler<GetConfimrationQuery, Result<ConfirmationResponse>, ManifestationRegistration>
    {
        public GetConfirmationQueryHandler(IGenericRepository<ManifestationRegistration> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<ConfirmationResponse>> Handle(GetConfimrationQuery request, CancellationToken cancellationToken)
        {
            var registration = Repository.GetQueryable()
                  .AsNoTracking()
                  .Where(x => x.Id == request.confirmationDto.Id)
                  .FirstOrDefault();

            ConfirmationResponse response;

            if (registration.EmailAddres == request.confirmationDto.Email &&
                registration.ReservationToken == request.confirmationDto.Identificator)
            {
                response = new ConfirmationResponse(true);
            }
            else
            {
                response = new ConfirmationResponse(false);
            }

            return Result<ConfirmationResponse>.Success(Mapper.Map<ConfirmationResponse>(response));
        }
    }
}
