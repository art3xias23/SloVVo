﻿using System;
using System.Windows.Input;

namespace SloVVo.ViewModels.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Action _executeNull;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null){}

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
           _canExecute = canExecute;
        }

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _executeNull = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
