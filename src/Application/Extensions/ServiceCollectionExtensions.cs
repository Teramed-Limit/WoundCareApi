namespace WoundCareApi.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(
                cfg =>
                    cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
            );

            return services;
        }
    }
}
