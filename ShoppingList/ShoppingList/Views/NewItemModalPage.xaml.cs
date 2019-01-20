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
	public partial class NewItemModalPage : ContentPage
	{
        private NewItemModalViewModel _viewModel => (NewItemModalViewModel)BindingContext;

		public NewItemModalPage ()
		{
			InitializeComponent();
            BindingContext = new NewItemModalViewModel();
		}

        public void OnOkClicked(object sender, EventArgs e)
        {
			_viewModel.OnOk();
        }

        public void OnCancelClicked(object sender, EventArgs e)
        {
			_viewModel.OnCancel();
        }
	}
}