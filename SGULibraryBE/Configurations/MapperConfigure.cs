﻿using Mapster;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Models;

namespace SGULibraryBE.Configurations
{
    public static class MapperConfigure
    {
        public static WebApplication ConfigureRequestMapper(this WebApplication app)
        {
            TypeAdapterConfig<AccountRequest, Account>.NewConfig()
                                                      .IgnoreIf((src, dest) => src.RoleId == 0, dest => dest.RoleId)
                                                      .IgnoreNullValues(true);

            TypeAdapterConfig<DeviceRequest, Device>.NewConfig()
                                                    .IgnoreNullValues(true);

            TypeAdapterConfig<BorrowDeviceRequest, BorrowDevice>.NewConfig()
                                                                .IgnoreNullValues(true)
                                                                .IgnoreIf((src, dest) => src.DateCreate == DateTime.MinValue, dest => dest.DateCreate)
                                                                .IgnoreIf((src, dest) => src.DateBorrow == DateTime.MinValue, dest => dest.DateBorrow)
                                                                .IgnoreIf((src, dest) => src.DateReturn == DateTime.MinValue, dest => dest.DateReturn!);

            TypeAdapterConfig<ViolationResquest, Violation>.NewConfig()
                                                           .IgnoreNullValues(true);

            TypeAdapterConfig<AccountViolationRequest, AccountViolation>.NewConfig()
                                                                        .Map(dest => dest.EStatus, src => src.Status)
                                                                        .IgnoreNullValues(true);

            TypeAdapterConfig<AccountViolation, AccountViolationResponse>.NewConfig()
                                                                         .Map(dest => dest.Status, src => src.EStatus);

            TypeAdapterConfig<ReservationRequest, Reservation>.NewConfig()
                                                              .IgnoreIf((src, dest) => src.DateCreate == DateTime.MinValue, dest => dest.DateCreate)
                                                              .IgnoreIf((src, dest) => src.DateBorrow == DateTime.MinValue, dest => dest.DateBorrow)
                                                              .IgnoreIf((src, dest) => src.DateReturn == DateTime.MinValue, dest => dest.DateReturn)
                                                              .IgnoreNullValues(true);

            return app;
        }
    }
}