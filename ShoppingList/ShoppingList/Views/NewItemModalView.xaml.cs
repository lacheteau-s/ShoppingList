using ShoppingList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewItemModalView : ContentPage
	{
        private NewItemModalViewModel _viewModel => (NewItemModalViewModel)BindingContext;

		public NewItemModalView ()
		{
			InitializeComponent();
		}

        public void OnOkClicked(object sender, EventArgs e)
        {
			_viewModel.OnOk();
        }

        public async void OnCancelClicked(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }

		public void OnIncrementQuantityClicked(object sender, EventArgs e)
		{
			_viewModel.IncrementQuantity();
		}

		public void OnDecrementQuantityClicked(object sender, EventArgs e)
		{
			_viewModel.DecrementQuantity();
		}
	}
}