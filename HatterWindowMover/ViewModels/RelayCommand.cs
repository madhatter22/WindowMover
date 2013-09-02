using System;
using System.Windows.Input;

namespace WindowMover
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;
        public RelayCommand(Action action) : this(action, null)
        {
        }

        public RelayCommand(Action action, Func<bool> canExecute)
        {
            if (action == null) throw new ArgumentNullException("action");
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}
