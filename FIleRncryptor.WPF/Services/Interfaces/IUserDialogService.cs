using System.Collections.Generic;

namespace FIleRncryptor.WPF.Services.Interfaces
{
    internal interface IUserDialogService
    {
        bool OpenFile(string Title, out string SelectedFile, string Filter = "Все файлы (*.*)|*.*");
        bool OpenFiles(string Title, out IEnumerable<string> SelectedFiles, string Filter = "Все файлы (*.*)|*.*");
    }
}
