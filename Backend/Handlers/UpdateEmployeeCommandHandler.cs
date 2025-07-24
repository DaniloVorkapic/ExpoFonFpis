using AutoMapper;
using Backend.Commands;
using Backend.DTOs;
using Backend.Entities;
using Backend.Http;
using Backend.Repositories;

namespace Backend.Handlers
{
    public class UpdateEmployeeCommandHandler : BaseCommandHandler<UpdateEmployeeCommand, Result<EmployeeDto>, Employee>
    {
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<Result<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await Repository.GetByIdAsync(request.EmployeeDto.EmployeeId);
            if (employee == null)
            {
                return Result<EmployeeDto>.Failure("Employee not found.");
            }

            employee.Update(request.EmployeeDto.FirstName, request.EmployeeDto.LastName, request.EmployeeDto.Description);

            await UnitOfWork.CommitAsync();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Result<EmployeeDto>.Success(employeeDto);
        }
    }
}
