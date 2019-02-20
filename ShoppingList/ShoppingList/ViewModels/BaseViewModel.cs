using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		public virtual Task InitializeAsync()
		{
			return Task.CompletedTask;
		}
	}
}
