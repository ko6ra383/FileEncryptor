using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using FIleRncryptor.WPF.Services;
using FIleRncryptor.WPF.ViewModels;

namespace FIleRncryptor.WPF
{
    public partial class App
    {
        private static IHost __Host;
        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IServiceProvider Services => Host.Services;
        internal static void Configureservices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddServices();
            services.AddViewModels();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (Host) await Host.StopAsync();
        }
    }
}
