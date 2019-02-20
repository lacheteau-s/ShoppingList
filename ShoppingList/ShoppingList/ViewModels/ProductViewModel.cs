using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
		public string Name { get; set; }

		public ProductViewModel(ProductModel product)
		{
			Name = product.Name;
		}
    }
}
