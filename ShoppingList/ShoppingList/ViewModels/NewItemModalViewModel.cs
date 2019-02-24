using ShoppingList.Core;
using ShoppingList.Data;
using ShoppingList.Data.Entities;
using ShoppingList.Models;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.ViewModels
{
    public class NewItemModalViewModel : BaseViewModel
    {
		private readonly IProductService _productService;
		private readonly IEventDispatcher _eventDispatcher;
		private readonly INavigationService _navigationService;

        private string _productName;
        private int _quantity;
        private double _price;

        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

		public string Subtotal => $"{Quantity * Price} €"; // TODO: currency configuration

		public NewItemModalViewModel(IProductService productService, IEventDispatcher eventDispatcher, INavigationService navigationService)
		{
			_productService = productService;
			_eventDispatcher = eventDispatcher;
			_navigationService = navigationService;

			ProductName = null;
			Quantity = 1;
			Price = 0;
		}

		public async Task OnOkAsync()
		{
			var model = new ProductModel()
			{
				Name = ProductName,
				Quantity = Quantity,
				Price = Price
			};

			await _productService.AddNewProductAsync(model);

			_eventDispatcher.Publish(Events.ItemAdded, model);
		}

		public Task CloseAsync()
		{
			return _navigationService.NavigateBackAsync();
		}

		public void IncrementQuantity()
		{
			if (Quantity < 100)
				Quantity++;
		}

		public void DecrementQuantity()
		{
			if (Quantity > 1)
				Quantity--;
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(Quantity), nameof(Subtotal));
			RegisterDependency(nameof(Price), nameof(Subtotal));
			// TODO: RegisterDependency the other way around: i.e., property a should be updated when properties a, b, ... are updated
		}
	}
}
