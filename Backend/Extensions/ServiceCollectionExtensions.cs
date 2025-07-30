using FluentValidation.AspNetCore;
using System.Net.NetworkInformation;
using System.Reflection;
using Backend.Commands;
using Backend.Data;
using Backend.DTOs;
using Backend.Events;
using Backend.Handlers;
using Backend.Http;
using Backend.Middleware;
using Backend.Middleware.Evidencija.Middleware;
using Backend.Queries;
using Backend.Repositories;
using Backend.Services;
using Backend.Validators;
using Hangfire;
using MediatR;
using FluentValidation;

namespace Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration options)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IReturnDateService, ReturnDateService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<GlobalExceptionMiddleware>();
            services.AddTransient<ValidationExceptionMiddleware>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage("Server=localhost;Database=PregnancyTrackerDb;User Id=admin;Password=admin123;Encrypt=False");
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection ConfigureCqrs(this IServiceCollection services, IConfiguration options)
        {
            services.AddTransient<IRequestHandler<GetAllEmployeesQuery, Result<List<EmployeeDto>>>, GetAllEmployeesQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllMaleEmployeesQuery, Result<List<MaleEmployeeDto>>>, GetAllMaleEmployeesQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllFemaleEmployeesQuery, Result<List<FemaleEmployeeDto>>>, GetAllFemaleEmployeesQueryHandler>();
            services.AddTransient<IRequestHandler<CreateEmployeeCommand, Result<EmployeeDto>>, CreateEmployeeCommandHandler>();
            services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>, GetEmployeeByIdQueryHandler>();
            services.AddTransient<IRequestHandler<UpdateEmployeeCommand, Result<EmployeeDto>>, UpdateEmployeeCommandHandler>();

            services.AddTransient<IRequestHandler<CreateChildCommand, Result<EmployeeDto>>, CreateChildCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllChildrenQuery, Result<List<ChildDto>>>, GetAllChildrenQueryHandler>();
            services.AddTransient<IRequestHandler<DeleteChildCommand, Result<Unit>>, DeleteChildCommandHandler>();

            services.AddTransient<IRequestHandler<CreateRegistrationCommand, Result<RegistrationResponse>>, CreateRegistrationCommandHandler>();

            services.AddTransient<IRequestHandler<GetAllPregnanciesQuery, Result<List<PregnancyDto>>>, GetAllPregnanciesQueryHandler>();
            services.AddTransient<IRequestHandler<CreatePregnancyCommand, Result<EmployeeDto>>, CreatePregnancyCommandHandler>();
            services.AddTransient<IRequestHandler<GetPregnancyByIdQuery, Result<PregnancyDto>>, GetPregnancyByIdQueryHandler>();
            services.AddTransient<IRequestHandler<UpdatePregnancyCommand, Result<PregnancyDto>>, UpdatePregnancyCommandHandler>();
            services.AddTransient<IRequestHandler<DeletePregnancyCommand, Result<Unit>>, DeletePregnancyCommandHandler>();

            services.AddTransient<IRequestHandler<CreateLeaveCommand, Result<PregnancyDto>>, CreateLeaveCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteLeaveCommand, Result<Unit>>, DeleteLeaveCommandHandler>();

            services.AddTransient<IRequestHandler<CreateHolidayCommand, Result<HolidayDto>>, CreateHolidayCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllHolidaysQuery, Result<List<HolidayDto>>>, GetAllHolidaysQueryHandler>();
            services.AddTransient<IRequestHandler<DeleteHolidayCommand, Result<Unit>>, DeleteHolidayCommandHandler>();

            services.AddTransient<IRequestHandler<CreateNotificationRecipientCommand, Result<Unit>>, CreateNotificationRecipientCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllNotificationRecipientsQuery, Result<List<NotificationRecipientDto>>>, GetAllNotificationRecipientsQueryHandler>();
            services.AddTransient<IRequestHandler<DeleteNotificationRecipientCommand, Result<Unit>>, DeleteNotificationRecipientCommandHandler>();

            services.AddTransient<IRequestHandler<GetAllExibitionsQuery, Result<List<ExibitionDto>>>, GetAllExibitionsQueryHandler>();
            services.AddTransient<IRequestHandler<GetFonManifestationQuery, Result<ManifestationDto>>, GetManifestationQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllRegistrationsQuery, Result<List<RegistrationDto>>>, GetAllRegistrationsQueryHandler>();
            services.AddTransient<IRequestHandler<GetPricesQuery, Result<PricesDto>>, GetPricesQueryHandler>();
            services.AddTransient<IRequestHandler<GetCalculatedPriceQuery, Result<CalculatedDto>>, GetCalculatedPriceQueryHandler>();
            services.AddTransient<IRequestHandler<GetRegistrationByIdQuery, Result<RegistrationByIdDto>>, GetRegistrationByIdQueryHandler>();


            //services.AddTransient<INotificationHandler<EmployeeUpdatedEvent>, EmployeeUpdatedEventHandler>();
            //services.AddTransient<INotificationHandler<HolidayUpdatedEvent>, HolidayUpdatedEventHandler>();
            //services.AddTransient<INotificationHandler<SendNotificationCommand>, SendNotificationCommandHandler>();

            return services;
        }

        public static IServiceCollection ConfigureValidators(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateChildCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateLeaveCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateHolidayCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateNotificationRecipientCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateRegistrationValidator>();
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("http://localhost:4200", "http://89.216.53.122")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }

        //public static IApplicationBuilder ConfigureNotificationJob(this IApplicationBuilder app)
        //{
        //    RecurringJob.AddOrUpdate<INotificationService>("send-daily-notification",x => x.SendEmailToRecipients(), Cron.Daily);

        //    return app;
        //}
    }
}
