using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.Core
{
	public class Command : ICommand
	{
		private readonly Action<object> _action;

		public event EventHandler CanExecuteChanged;

		public Command(Action action) : this(o => action())
		{
			if (action == null)
				throw new ArgumentNullException();
		}

		public Command(Action<object> action)
		{
			_action = action ?? throw new ArgumentNullException();
		}

		public bool CanExecute(object parameter)
		{
			return true; // TODO
		}

		public void Execute(object parameter)
		{
			_action?.Invoke(parameter);
		}
	}
}
