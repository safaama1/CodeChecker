using System;

using System.Windows.Input;

namespace CodeCheckerClient.Core
{
    class RelayCommand:ICommand
    {
        private Action<object> _execute;
        private Func<object,bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested+=value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object paramaeter)
        {
            return _canExecute == null || _canExecute(paramaeter);

        }

        public void Execute(object paramaeter)
        {
           _execute(paramaeter);

        }
    }
}
