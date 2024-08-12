using AutoMapper;
namespace backend.AutoMapperProfiles
{
    public static class AutoMapperStatup
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
