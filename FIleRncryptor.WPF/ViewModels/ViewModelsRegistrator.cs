using Microsoft.Extensions.DependencyInjection;
namespace FIleRncryptor.WPF.ViewModels
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>();
    }
}
