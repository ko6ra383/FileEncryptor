using FIleRncryptor.WPF.Infrastructure.Commands;
using FIleRncryptor.WPF.Services.Interfaces;
using FIleRncryptor.WPF.ViewModels.Base;
using System;
using System.IO;
using System.Windows.Input;

namespace FIleRncryptor.WPF.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IUserDialogService _UserDialog;
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
        }

        #endregion
        #endregion
        public MainViewModel(IUserDialogService UserDialog)
        {
            _UserDialog = UserDialog;
        }
    }
}
