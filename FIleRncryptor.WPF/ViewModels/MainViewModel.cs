using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIleRncryptor.WPF.ViewModels.Base;

namespace FIleRncryptor.WPF.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Properties
        #region Title
        private string _Title = "Encryptor";
        public string Title { get => _Title; set => Set(ref _Title, value); }
        #endregion  
        #endregion
    }
}
