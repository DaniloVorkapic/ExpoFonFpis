using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Enums;
using Backend.Http;
using Backend.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Backend.Handlers
{
    public class DeactivateRegistrationCommandHandler : BaseCommandHandler<DeactivateRegistrationCommand, Result<DeactivateRegistrationResponse>, ManifestationRegistration>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateRegistrationCommand> _validator;

        public DeactivateRegistrationCommandHandler(IMapper mapper, IMediator mediator, IGenericRepository<ManifestationRegistration> repository, IUnitOfWork unitOfWork, IValidator<CreateRegistrationCommand> validator)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
        }

        public override async Task<Result<DeactivateRegistrationResponse>> Handle(DeactivateRegistrationCommand request, CancellationToken cancellationToken)
        {
            var originalRegistration = Repository.GetQueryable()
                .Include(x => x.PromoCodeGenerated)
                .AsTracking()
                .FirstOrDefault(x => x.Id == request.registrationDto.RegistrationId);

            if (originalRegistration == null)
            {
                return Result<DeactivateRegistrationResponse>.Failure("You cannot deactivate registration which not exists!");
            }

            originalRegistration.DeactivateRegistration((int)LifeCycleStatusEnum.Deactivated);

            var rSave = await Repository.UpdateAsync(originalRegistration);
            if (!rSave)
            {
                return Result<DeactivateRegistrationResponse>.Failure("Cannot deactivate registration.");
            }
            if (originalRegistration.PromoCodeGenerated.LifecycleStatus.Value == (int)LifeCycleStatusEnum.Active)
            {


                using (var connection = new SqlConnection("data source=DANILOSSOE;Initial Catalog=FonExpo;Integrated Security=True;TrustServerCertificate=True;"))
                using (var command = new SqlCommand("UpdatePromoCodeStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PromoCode", originalRegistration.PromoCodeGenerated.Code);
                    command.Parameters.AddWithValue("@LifecycleStatus", (int)LifeCycleStatusEnum.Deactivated);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            await UnitOfWork.CommitAsync();
            return Result<DeactivateRegistrationResponse>.Success(new DeactivateRegistrationResponse());
        }
    }
}
