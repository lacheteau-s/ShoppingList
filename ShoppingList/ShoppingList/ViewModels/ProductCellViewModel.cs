using ShoppingList.Core;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModels
{
    public class ProductCellViewModel : BaseViewModel
    {
		public string Name { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public string QuantityAndName => $"{Quantity} {Name}";

		public string TotalPrice => Price == 0 ? "-" : $"{((Quantity > 1) ? Price * Quantity : Price)}€";

		public string UnitPrice => (Price > 0 && Quantity > 1) ? $"({Price}€ / unit)" : "";

		private bool _isSelectable;

		public bool IsSelectable
		{
			get { return _isSelectable; }
			set { SetProperty(ref _isSelectable, value); }
		}

		private bool _isSelected;

		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetProperty(ref _isSelected, value); }
		}

		public ICommand CheckedCommand => new Command(() => IsSelected = !IsSelected);

		public ProductCellViewModel(ProductModel product, bool isSelectable = false)
		{
			Name = product.Name;
			Quantity = product.Quantity;
			Price = product.Price;
			IsSelectable = isSelectable;
			IsSelected = true;
			PropertyChanged += ProductCellViewModel_PropertyChanged;
		}

		private void ProductCellViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
		}
	}
}
