using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MaleEmployee, EmployeeDto>();
            CreateMap<MaleEmployee, MaleEmployeeDto>();
            CreateMap<FemaleEmployee, EmployeeDto>();
            CreateMap<FemaleEmployee, FemaleEmployeeDto>();

            CreateMap<Child, ChildDto>();
            CreateMap<CreateChildDto, Child>();

            CreateMap<Pregnancy, PregnancyDto>();
            CreateMap<CreatePregnancyDto, Pregnancy>();

            CreateMap<Leave, LeaveDto>();
            CreateMap<CreateLeaveDto, Leave>();

            CreateMap<Holiday, HolidayDto>();
            CreateMap<CreateHolidayDto, Holiday>();

            CreateMap<NotificationRecipient, NotificationRecipientDto>();
        }
    }
}
