using System;

namespace Tablut.ViewModel
{
    public class DelegateCommand
    {
        #region Private Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructors

        public DelegateCommand(Action<object> execute) : this(execute, null) { }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        #endregion

        #region Public Methods

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}