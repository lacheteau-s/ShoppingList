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
		NavigationPage _navigationPage => (NavigationPage)Application.Current.MainPage;

		INavigation _navigation => _navigationPage.Navigation;

		private static readonly Dictionary<string, Type> _mapping = new Dictionary<string, Type>();

		public void Map<TViewModel, TView>()
		{
			_mapping.Add(typeof(TViewModel).Name, typeof(TView));
		}

		public Task NavigateToAsync<TViewModel>(bool isModal = false)
		{
			var viewType = _mapping[typeof(TViewModel).Name];
			var viewInstance = (Page)IoC.GetInstance(viewType);

			if (isModal)
				return _navigation.PushModalAsync(viewInstance);
			else
				return _navigationPage.PushAsync(viewInstance);
		}

		public Task NavigateBackAsync()
		{
			// Xamarin Forms' INavigation contains 2 separate stacks for modal and regular views.
			// The following schema assumes that when the ModalStack > 0, the currently displayed view is a modal
			// The following scenario would be problematic: Home > Navigate to modal (ModalStack = 1) > Navigate to regular view > Navigate to regular view?
			if (_navigation.ModalStack.Count > 0)
				return _navigation.PopModalAsync();
			else
				return _navigationPage.PopAsync();
		}
	}
}
