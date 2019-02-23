using ShoppingList.Core;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingList.Services
{
	public class NavigationService : INavigationService
	{
		INavigation _navigation => Application.Current.MainPage.Navigation;

		private static readonly Dictionary<string, Type> _mapping = new Dictionary<string, Type>();

		public void Map<TViewModel, TView>()
		{
			_mapping.Add(nameof(TViewModel), typeof(TView));
		}

		public Task NavigateToAsync<TViewModel>(bool isModal = false)
		{
			var viewType = _mapping[nameof(TViewModel)];
			var viewInstance = (Page)IoC.GetInstance(viewType);

			if (isModal)
				return _navigation.PushModalAsync(viewInstance);
			else
				return _navigation.PushAsync(viewInstance);
		}
	}
}
