using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Enums;
using Backend.Http;
using Backend.Repositories;
using FluentValidation;
using JordiAragon.SharedKernel.Contracts.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Backend.Handlers
{
    public class CreateRegistrationCommandHandler : BaseCommandHandler<CreateRegistrationCommand, Result<RegistrationResponse>, ManifestationRegistration>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateRegistrationCommand> _validator;

        public CreateRegistrationCommandHandler(IMapper mapper, IMediator mediator, IGenericRepository<ManifestationRegistration> repository, IUnitOfWork unitOfWork, IValidator<CreateRegistrationCommand> validator)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
        }

        public override async Task<Result<RegistrationResponse>> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
              var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<RegistrationResponse>.Failure(string.Join(" | ", errors));
            }

            var rValidate = ValidateCapacity(request.registrationDto.NumberOfPeople, request.registrationDto.IsPhotoReserved, request.registrationDto.IsArtReserved, request.registrationDto.Id);

            if (!rValidate.IsSuccess)
            {
                return Result<RegistrationResponse>.Failure(rValidate.Error);
            }

            var rRegistrationToSave = PrepareRegistration(request.registrationDto);

            

            if (!rRegistrationToSave.IsSuccess)
            {
                return Result<RegistrationResponse>.Failure("aa");
            }

           // var rSave = SaveRegistrationAsync(rRegistrationToSave.Value);

            var rSave = await SaveRegistrationAsync(rRegistrationToSave.Value, request.registrationDto.HasPromoCode);

            if (!rSave.IsSuccess)
            {
                return Result<RegistrationResponse>.Failure(rSave.Error);
            }

            return Result<RegistrationResponse>.Success(new RegistrationResponse(rRegistrationToSave.Value.PromoCodeGenerated.Code));
        }

        private Result<bool> ValidateCapacity(int numberOfPeople, bool isPhotoReserved, bool isArtReserved, long? registrationId)
        {
            registrationId = registrationId == 0 ? null : registrationId;

            var data = Repository.GetQueryable()
                .AsNoTracking()
                .Where(x => x.ManifestationId == 1)
                .Where(x => !registrationId.HasValue || x.Id != registrationId.Value)
                .GroupBy(x => x.Manifestation)
                .Select(g => new
                {
                    Capacity = g.Key.Capacity ?? 0,
                    PhotoReserved = g.Where(x => x.IsPhotoReserved).Sum(x => x.NumberOfPeople ?? 0),
                    ArtReserved = g.Where(x => x.IsArtReserved).Sum(x => x.NumberOfPeople ?? 0)
                })
                .SingleOrDefault();

            if (data == null)
            {
                return Result<bool>.Success(true);
            }

            if (isPhotoReserved && (data.Capacity - data.PhotoReserved) < numberOfPeople)
            {
                return Result<bool>.Failure("You will exceed number of people that can be at photo day.");
            }

            if (isArtReserved && (data.Capacity - data.ArtReserved) < numberOfPeople)
            {
                return Result<bool>.Failure("You will exceed number of people that can be at art day.");
            }

            return Result<bool>.Success(true);
        }

        private async Task<Result<bool>> SaveRegistrationAsync(ManifestationRegistration registration, bool hasPromoCode)
        {
            var rSave = registration.Id == 0 ? await Repository.CreateAsync(registration) : await Repository.UpdateAsync(registration);
          

            if (!rSave)
            {
                return  Result<bool>.Failure("Cannot save registration.");
            }
            if (hasPromoCode)
            {


                using (var connection = new SqlConnection("data source=DANILOSSOE;Initial Catalog=FonExpo;Integrated Security=True;TrustServerCertificate=True;"))
                using (var command = new SqlCommand("UpdatePromoCodeStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PromoCode", registration.PromoCodeUsed);
                    command.Parameters.AddWithValue("@LifecycleStatus", (int)LifeCycleStatusEnum.Used);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            await UnitOfWork.CommitAsync();
            return Result<bool>.Success(rSave);
        }

        private Result<ManifestationRegistration> PrepareRegistration(CreateRegistrationDto registrationDto)
        {

            if (registrationDto.Id == 0)
            {
                //var manifestationId = Repository.GetQueryable()
                //  .Include(x => x.Manifestation)
                //  .Where(x => x.Manifestation.Name == "FonExpo")
                //  .Select(x => x.Manifestation.Id)
                //  .SingleOrDefault();


                var code = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                var promoCode = PromoCode.Create(code, 1);


                var newRegistration = ManifestationRegistration.Create(1, registrationDto.FirstName, registrationDto.LastName, registrationDto.Profession, registrationDto.Address, registrationDto.Email,
                    registrationDto.IsPhotoReserved, registrationDto.IsArtReserved, registrationDto.IsGroupRegistration, registrationDto.Price, registrationDto.NumberOfPeople, registrationDto.PromoCode, registrationDto.HasPromoCode, promoCode);

                return Result<ManifestationRegistration>.Success(newRegistration);
            }

            return UpdateRegistration(registrationDto);
        }

        private Result<ManifestationRegistration> UpdateRegistration(CreateRegistrationDto registrationDto)
        {
            var originalRegistration = Repository.GetQueryable()
                   .AsTracking()
                   .FirstOrDefault(x => x.Id == registrationDto.Id);

            if (originalRegistration == null)
            {
                return Result<ManifestationRegistration>.Failure("Registration did not found.");
            }

            originalRegistration = originalRegistration.Update(registrationDto.FirstName, registrationDto.LastName, registrationDto.Profession, registrationDto.Address, registrationDto.Email,
                    registrationDto.IsPhotoReserved, registrationDto.IsArtReserved, registrationDto.IsGroupRegistration, registrationDto.Price, registrationDto.NumberOfPeople);


             return Result<ManifestationRegistration>.Success(originalRegistration);
        }
    }
}
