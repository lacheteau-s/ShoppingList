using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public interface IProductService
	{
		Task AddNewProductAsync(ProductModel product);
	}
}
