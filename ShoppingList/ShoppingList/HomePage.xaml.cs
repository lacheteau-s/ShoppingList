using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingList
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		private async void OnAddNewItemClicked(object sender, EventArgs e)
		{
			var page = new NewItemModalPage();

			await Navigation.PushModalAsync(page);
		}
	}
}
