﻿using FIleRncryptor.WPF.Infrastructure.Commands.Base;
using System;

namespace FIleRncryptor.WPF.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;

        public LambdaCommand(Action Execute, Func<bool> CanExecute = null)
            :this(p => Execute(), CanExecute is null ? (Func<object, bool>)null : p => CanExecute())
        {

        }
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute;
            _CanExecute = CanExecute;
        }

        protected override void Execute(object parameter) => _Execute(parameter);
        protected override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
    }
}
