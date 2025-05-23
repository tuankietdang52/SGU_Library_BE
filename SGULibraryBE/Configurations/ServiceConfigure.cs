﻿using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SGULibraryBE.Exceptions;
using SGULibraryBE.Models.Commons;
using SGULibraryBE.Repositories;
using SGULibraryBE.Repositories.Implementations;
using SGULibraryBE.Services;
using SGULibraryBE.Services.Implementations;

namespace SGULibraryBE.Configurations
{
    public static class ServiceConfigure
    {
        public static IServiceCollection AllowCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            return services;
        }

        public static IServiceCollection RegisterDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            string? connection = configuration.GetConnectionString("MyDb");
            NullException.ThrowIfNull(connection);

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseMySQL(connection));

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IBorrowDeviceService, BorrowDeviceService>();
            services.AddScoped<IViolationService, ViolationService>();
            services.AddScoped<IAccountViolationService, AccountViolationService>();
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IBorrowDeviceRepository, BorrowDeviceRepository>();
            services.AddScoped<IViolationRepository, ViolationRepository>();
            services.AddScoped<IAccountViolationRepository, AccountViolationRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
