using ShoppingList.Core;
using ShoppingList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingList.Views
{
	public partial class HomeView : ContentPage
	{
        private HomeViewModel _viewModel => (HomeViewModel)BindingContext;

		public HomeView()
		{
			InitializeComponent();
		}

		private async void OnAddNewItemClicked(object sender, EventArgs e)
		{
			var page = IoC.GetInstance<NewItemModalView>();

			await Navigation.PushModalAsync(page);
		}
	}
}
