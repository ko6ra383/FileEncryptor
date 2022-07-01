using FIleRncryptor.WPF.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FIleRncryptor.WPF.Services
{
    internal static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialogService, UserDialogService>();
    }
}
