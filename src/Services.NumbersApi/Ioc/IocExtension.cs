using Microsoft.Extensions.DependencyInjection;
using Services.NumbersApi.Interfaces;

namespace Services.NumbersApi.Ioc
{
    public static class IocExtension
    {
        public static void RegisterNumbersApi(this IServiceCollection services)
        {
            services.AddTransient<INumbersApi, NumbersApi>();
        }
    }
}
