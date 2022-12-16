using System;
using System.Windows.Input;

namespace Bookcase.ViewModel
{
    public class Command : ICommand
    {
        Action<object?> execute;
        Func<object?, bool>? canExecute;

        /// <summary>
        /// Event when conditions change
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        /// <summary>
        /// Validate For Command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        /// <summary>
        /// Execute Command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
