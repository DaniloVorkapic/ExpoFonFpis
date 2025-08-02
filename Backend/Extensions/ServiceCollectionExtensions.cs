using Backend.Commands;
using Backend.Data;
using Backend.DTOs;
using Backend.Handlers;
using Backend.Http;
using Backend.Middleware;
using Backend.Middleware.Evidencija.Middleware;
using Backend.Queries;
using Backend.Repositories;
using Backend.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration options)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
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
            services.AddTransient<IRequestHandler<CreateRegistrationCommand, Result<RegistrationResponse>>, CreateRegistrationCommandHandler>();
            services.AddTransient<IRequestHandler<DeactivateRegistrationCommand, Result<DeactivateRegistrationResponse>>, DeactivateRegistrationCommandHandler>();

            services.AddTransient<IRequestHandler<GetAllExibitionsQuery, Result<List<ExibitionDto>>>, GetAllExibitionsQueryHandler>();
            services.AddTransient<IRequestHandler<GetFonManifestationQuery, Result<ManifestationDto>>, GetManifestationQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllRegistrationsQuery, Result<List<RegistrationDto>>>, GetAllRegistrationsQueryHandler>();
            services.AddTransient<IRequestHandler<GetPricesQuery, Result<PricesDto>>, GetPricesQueryHandler>();
            services.AddTransient<IRequestHandler<GetCalculatedPriceQuery, Result<CalculatedDto>>, GetCalculatedPriceQueryHandler>();
            services.AddTransient<IRequestHandler<GetRegistrationByIdQuery, Result<RegistrationByIdDto>>, GetRegistrationByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetConfimrationQuery, Result<ConfirmationResponse>>, GetConfirmationQueryHandler>();

            return services;
        }

        public static IServiceCollection ConfigureValidators(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
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
    }
}
