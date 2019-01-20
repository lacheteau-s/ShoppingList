using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class NewItemModalViewModel : ObservableObject
    {
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

		public NewItemModalViewModel()
		{
			ProductName = null;
			Quantity = 1;
			Price = 0;
		}

		public void OnOk()
		{
			var model = new ProductModel()
			{
				ProductName = ProductName,
				Quantity = Quantity,
				Price = Price
			};
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
