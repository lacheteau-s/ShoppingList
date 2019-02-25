﻿using ShoppingList.Data;
using ShoppingList.Data.Entities;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<List<ProductModel>> GetSelectedProducts()
		{
			var products = await _productRepository.GetAsync(x => x.Selected);

			return products.Select(p => new ProductModel
			{
				// TODO: Id in model?
				Name = p.Name,
				Price = p.Price,
				Quantity = p.Quantity,
			}).ToList();
		}

		public async Task<bool> HasUnselectedProducts()
		{
			var first = await _productRepository.AsQueryable().FirstOrDefaultAsync(x => !x.Selected);

			return first != null;
		}
	}
}
