using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Events;
using Backend.Http;
using Backend.Repositories;
using MediatR;
using FluentValidation;

namespace Backend.Handlers
{
    public class CreateEmployeeCommandHandler : BaseCommandHandler<CreateEmployeeCommand, Result<EmployeeDto>, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        public CreateEmployeeCommandHandler(IMapper mapper, IMediator mediator, IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IValidator<CreateEmployeeCommand> validator) 
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
        }

        public override async Task<Result<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            Employee newEmployee;
            if (request.EmployeeDto.Gender.Equals("male", StringComparison.CurrentCultureIgnoreCase))
            {
                newEmployee = MaleEmployee.Create(request.EmployeeDto.FirstName, request.EmployeeDto.LastName, request.EmployeeDto.Description);
            }
            else
            {
                newEmployee = FemaleEmployee.Create(request.EmployeeDto.FirstName, request.EmployeeDto.LastName, request.EmployeeDto.Description);
            }

            await Repository.CreateAsync(newEmployee);
            await UnitOfWork.CommitAsync();

            await _mediator.Publish(new EmployeeUpdatedEvent(newEmployee.Id), cancellationToken);

            var employeeDto = _mapper.Map<EmployeeDto>(newEmployee);
            return Result<EmployeeDto>.Success(employeeDto);
        }
    }
}
