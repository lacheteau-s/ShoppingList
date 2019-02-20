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

		public ObservableCollection<ProductViewModel> Products { get; set; }

		public HomeViewModel(IEventDispatcher eventDispatcher, IProductService productService)
		{
			_eventDispatcher = eventDispatcher;
			_productService = productService;

			Products = new ObservableCollection<ProductViewModel>();
		}

		public override async Task InitializeAsync()
		{
			var selected = await _productService.GetSelectedProducts();

			if (selected.Any())
				Products = new ObservableCollection<ProductViewModel>(selected.Select(m => new ProductViewModel(m)));
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

			Products.Add(new ProductViewModel((ProductModel)payload));
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(Products), nameof(HasItems));
		}
	}
}
