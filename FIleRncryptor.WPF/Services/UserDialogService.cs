﻿using FIleRncryptor.WPF.Services.Interfaces;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FIleRncryptor.WPF.Services
{
    internal class UserDialogService : IUserDialogService
    {
        

        public bool OpenFile(string Title, out string SelectedFile, string Filter = "Все файлы (*.*)|*.*")
        {
            var file_dialog = new OpenFileDialog
            {
                Title = Title,
                Filter = Filter
            };
            if(file_dialog.ShowDialog() != true)
            {
                SelectedFile = null;
                return false;
            }
            SelectedFile = file_dialog.FileName; 
            return true;
        }
         
        public bool OpenFiles(string Title, out IEnumerable<string> SelectedFiles, string Filter = "Все файлы (*.*)|*.*")
        {
            var file_dialog = new OpenFileDialog
            {
                Title = Title,
                Filter = Filter
            };
            if(file_dialog.ShowDialog() != true)
            {
                SelectedFiles = Enumerable.Empty<string>();
                return false;
            }
            SelectedFiles = file_dialog.FileNames;
            return true;
        }

        public bool SaveFile(string Title, out string SelectedFile, string DefaultFileName = null, string Filter = "Все файлы (*.*)|*.*")
        {
            var file_dialog = new SaveFileDialog
            {
                Title = Title,
                Filter = Filter
            };
            if (!string.IsNullOrEmpty(DefaultFileName))
                file_dialog.FileName = DefaultFileName;

            if (file_dialog.ShowDialog() != true)
            {
                SelectedFile = null;
                return false;
            }
            SelectedFile = file_dialog.FileName;
            return true;
        }
        public void Error(string Title, string Message)=>
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);

        public void Information(string Title, string Message) =>
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Information);

        public void Warning(string Title, string Message)=>
            MessageBox.Show(Message,Title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
