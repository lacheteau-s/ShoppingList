using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
		public string Name { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public string QuantityAndName => $"{Quantity} {Name}";

		public string TotalPrice => Price == 0 ? "-" : $"{((Quantity > 1) ? Price * Quantity : Price)}€";

		public string UnitPrice => (Price > 0 && Quantity > 1) ? $"({Price}€ / unit)" : "";

		public ProductViewModel(ProductModel product)
		{
			Name = product.Name;
			Quantity = product.Quantity;
			Price = product.Price;
		}
    }
}
