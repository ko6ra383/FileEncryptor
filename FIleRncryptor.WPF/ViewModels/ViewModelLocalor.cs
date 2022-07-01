using Microsoft.Extensions.DependencyInjection;

namespace FIleRncryptor.WPF.ViewModels
{
    internal class ViewModelLocalor
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
    }
}
