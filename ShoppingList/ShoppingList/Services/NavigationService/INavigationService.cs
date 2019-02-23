using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public interface INavigationService
	{
		void Map<TViewModel, TView>();
		Task NavigateToAsync<TViewModel>(bool isModal = false);
	}
}
