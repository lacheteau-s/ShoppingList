using ShoppingList.Core;
using ShoppingList.Models;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
		private readonly IEventDispatcher _eventDispatcher;
		private readonly IProductService _productService;

		public bool HasItems => Products.Any();

		public ObservableCollection<ProductCellViewModel> Products { get; set; }

		public HomeViewModel(IEventDispatcher eventDispatcher, IProductService productService)
		{
			_eventDispatcher = eventDispatcher;
			_productService = productService;

			Products = new ObservableCollection<ProductCellViewModel>();
		}

		public override async Task InitializeAsync()
		{
			var selected = await _productService.GetSelectedProducts();

			if (selected.Any())
				Products = new ObservableCollection<ProductCellViewModel>(selected.Select(m => new ProductCellViewModel(m)));
		}

		public void Subscribe()
		{
			_eventDispatcher.Subscribe(Events.ItemAdded, this, OnNewItemAdded);
		}

		public void Unsubscribe()
		{
			_eventDispatcher.Unsubscribe(Events.ItemAdded, this);
		}

		private void OnNewItemAdded(object payload)
		{
			Unsubscribe();

			Products.Add(new ProductCellViewModel((ProductModel)payload));
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(Products), nameof(HasItems));
		}
	}
}
