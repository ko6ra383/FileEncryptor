using FIleRncryptor.WPF.Infrastructure.Commands;
using FIleRncryptor.WPF.Services.Interfaces;
using FIleRncryptor.WPF.ViewModels.Base;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace FIleRncryptor.WPF.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private const string __EncryptedFileSuffix = ".encrypted";
        private readonly IUserDialogService _UserDialog;
        private readonly IEncryptor _Encryptor;
        #region Properties
        #region Title
        private string _Title = "Encryptor";
        public string Title { get => _Title; set => Set(ref _Title, value); }
        #endregion
        #region Password
        private string _Password = "123";
        public string Password { get => _Password; set => Set(ref _Password, value); }
        #endregion
        #region SelectedFile
        private FileInfo _SelectedFile;
        public FileInfo SelectedFile { get => _SelectedFile; set => Set(ref _SelectedFile, value); }
        #endregion
        #endregion

        #region Commands
        #region SelectFileCommand
        private ICommand _SelectFileCommand;

        public ICommand SelectFileCommand => _SelectFileCommand ??= new LambdaCommand(OnSelecteFileCommandExecute);

        private void OnSelecteFileCommandExecute()
        {
            if (!_UserDialog.OpenFile("Выбор файла для шифрования", out var file_path)) return;
            var selected_file = new FileInfo(file_path);
            SelectedFile = selected_file.Exists ? selected_file : null;
        }
        #endregion
        #region EncryptCommand
        private ICommand _EncryptCommand;

        public ICommand EncryptCommand => _EncryptCommand ??= new LambdaCommand(OnEncryptCommandExecute, CanEncryptCommandExecute);

        private bool CanEncryptCommandExecute(object p) => 
            (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);

        private void OnEncryptCommandExecute(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;

            var defaultFile = file.FullName + __EncryptedFileSuffix;
            if (!_UserDialog.SaveFile("Выбор файла для сохранения", out var destinationPath, defaultFile)) return;

            var timer = Stopwatch.StartNew();
            _Encryptor.Encrypt(file.FullName, destinationPath, Password);
            timer.Stop();
            _UserDialog.Information("Шифрование", $"Шифрование файла успешно выполнено {timer.Elapsed.TotalSeconds}");
        }

        #endregion
        #region DecryptCommand
        private ICommand _DecryptCommand;

        public ICommand DecryptCommand => _DecryptCommand ??= new LambdaCommand(OnDecryptCommandExecute, CanDecryptCommandExecute);


        private bool CanDecryptCommandExecute(object p) =>
             (p is FileInfo file && file.Exists || SelectedFile != null) && !string.IsNullOrWhiteSpace(Password);

        private void OnDecryptCommandExecute(object p)
        {
            var file = p as FileInfo ?? SelectedFile;
            if (file is null) return;

            var defaultFile = file.FullName.EndsWith(__EncryptedFileSuffix)
                ? file.FullName.Substring(0, file.FullName.Length - __EncryptedFileSuffix.Length)
                : file.FullName;
            if (!_UserDialog.SaveFile("Выбор файла для сохранения", out var destinationPath, defaultFile)) return;

            var timer = Stopwatch.StartNew();
            var success = _Encryptor.Decrypt(file.FullName, destinationPath, Password);
            timer.Stop();
            if (success)
                _UserDialog.Information("Дешифрование", $"Дешифровка файла успешно выполнена {timer.Elapsed.TotalSeconds}");
            else
                _UserDialog.Error("Дешифрование", "Ошибка при дешифровке файла");
        }

        #endregion
        #endregion
        public MainViewModel(IUserDialogService UserDialog, IEncryptor Encryptor)
        {
            _UserDialog = UserDialog;
            _Encryptor = Encryptor;
        }
    }
}
