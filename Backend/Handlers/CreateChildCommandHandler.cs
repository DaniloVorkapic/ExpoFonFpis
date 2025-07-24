using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;
using FluentValidation;

namespace Backend.Handlers
{
    public class CreateChildCommandHandler : BaseCommandHandler<CreateChildCommand, Result<EmployeeDto>, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateChildCommand> _validator;

        public CreateChildCommandHandler(IMapper mapper, IGenericRepository<Employee> employeeRepository, IUnitOfWork unitOfWork, IValidator<CreateChildCommand> validator)
            : base(employeeRepository, unitOfWork)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public override async Task<Result<EmployeeDto>> Handle(CreateChildCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var employee = await Repository.GetByIdAsync(request.ChildDto.EmployeeId);
            if (employee == null)
            {
                return Result<EmployeeDto>.Failure("Employee not found.");
            }

            var child = _mapper.Map<Child>(request.ChildDto);
            employee.Children.Add(child);

            await UnitOfWork.CommitAsync();

            var updatedEmployee = await Repository.GetByIdAsync(request.ChildDto.EmployeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(updatedEmployee);

            return Result<EmployeeDto>.Success(employeeDto);
        }
    }
}
