using ShoppingList.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ShoppingList.ViewModels
{
    public class ProductInventoryViewModel : BaseViewModel
    {
		public ObservableCollection<ProductCellViewModel> Products { get; set; }

		public bool HasItems => Products.Any();

		private bool _isMultiselectActive;

		public bool IsMultiselectActive
		{
			get { return _isMultiselectActive; }
			set { SetProperty(ref _isMultiselectActive, value); }
		}

		public ICommand ToggleSelectionModeCommand => new Command(ToggleSelectionMode);

		public ProductInventoryViewModel()
		{
			IsMultiselectActive = false;

			Products = new ObservableCollection<ProductCellViewModel>()
			{
				new ProductCellViewModel(new Models.ProductModel { Name = "Product 1", Price = 1, Quantity = 1 }, IsMultiselectActive),
				new ProductCellViewModel(new Models.ProductModel { Name = "Product 2", Price = 2, Quantity = 2 }, IsMultiselectActive),
				new ProductCellViewModel(new Models.ProductModel { Name = "Product 3", Price = 3, Quantity = 3 }, IsMultiselectActive),
			};
		}

		private void ToggleSelectionMode()
		{
			IsMultiselectActive = !IsMultiselectActive;

			foreach (var product in Products)
				product.IsSelectable = IsMultiselectActive;
		}
    }
}
