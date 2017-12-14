using System;
using System.Windows.Input;

namespace dotNet.Helpers
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute;
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter) => _action();
    }
}