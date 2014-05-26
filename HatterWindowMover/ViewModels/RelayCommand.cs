using System;
using System.Windows.Input;

namespace WindowMover.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;
        private bool _canExecuteState;

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
            var canExecute = _canExecute == null || _canExecute();
            if (CanExecuteChanged != null && canExecute != _canExecuteState)
            {
                _canExecuteState = canExecute;
                CanExecuteChanged(this, new EventArgs());
            }
            else
            {
                _canExecuteState = canExecute;
            }
            
            return canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RelayCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter> _action;
        private readonly Func<TParameter, bool> _canExecute;
        private bool _canExecuteState;

        public RelayCommand(Action<TParameter> action)
            : this(action, null)
        {
        }

        public RelayCommand(Action<TParameter> action, Func<TParameter, bool> canExecute)
        {
            if (action == null) throw new ArgumentNullException("action");
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var canExecute = _canExecute == null || _canExecute((TParameter)parameter);
            if (CanExecuteChanged != null && canExecute != _canExecuteState)
            {
                _canExecuteState = canExecute;
                CanExecuteChanged(this, new EventArgs());
            }
            else
            {
                _canExecuteState = canExecute;
            }

            return canExecute;
        }

        public void Execute(object parameter)
        {
            _action((TParameter)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
