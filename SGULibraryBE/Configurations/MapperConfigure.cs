using Mapster;
using SGULibraryBE.DTOs.Requests;
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

            return app;
        }
    }
}