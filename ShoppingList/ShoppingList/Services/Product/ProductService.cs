using ShoppingList.Data;
using ShoppingList.Data.Entities;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public class ProductService : IProductService
	{
		private readonly IAsyncRepository<ProductEntity> _productRepository;

		public ProductService(IAsyncRepository<ProductEntity> productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task AddNewProductAsync(ProductModel product)
		{
			var entity = new ProductEntity
			{
				Name = product.Name,
				Quantity = product.Quantity,
				Price = product.Price,
				Selected = true
			};

			var id = await _productRepository.InsertAsync(entity);
			var item = await _productRepository.GetAsync(id);
		}
	}
}
