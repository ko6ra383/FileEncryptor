using FIleRncryptor.WPF.Infrastructure.Commands.Base;
using System.Windows;

namespace FIleRncryptor.WPF.Infrastructure.Commands
{
    internal class CloseWindow : Command
    {
        protected override void Execute(object parameter) =>
            (parameter as Window ?? App.FocucedWindow ?? App.ActiveWindow)?.Close();
    }
}
