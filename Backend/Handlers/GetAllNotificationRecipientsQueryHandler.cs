using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Queries;
using Backend.Repositories;
using MediatR;

namespace Backend.Handlers
{
    public class GetAllNotificationRecipientsQueryHandler : BaseQueryHandler<GetAllNotificationRecipientsQuery, Result<List<NotificationRecipientDto>>, NotificationRecipient>
    {
        public GetAllNotificationRecipientsQueryHandler(IGenericRepository<NotificationRecipient> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<Result<List<NotificationRecipientDto>>> Handle(GetAllNotificationRecipientsQuery request, CancellationToken cancellationToken)
        {
            var notificationRecipients = await Repository.GetAllAsync();
            return Result<List<NotificationRecipientDto>>.Success(Mapper.Map<List<NotificationRecipientDto>>(notificationRecipients));
        }
    }
}
