using ShoppingList.Core;
using ShoppingList.Models;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingList.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
		private readonly IEventDispatcher _eventDispatcher;
		private readonly IProductService _productService;
		private readonly INavigationService _navigationService;

		public bool HasItems => Products.Any();

		public ObservableCollection<ProductCellViewModel> Products { get; set; }

		public ICommand AddItemCommand => new Command(OnAddItem);

		public HomeViewModel(IEventDispatcher eventDispatcher, IProductService productService, INavigationService navigationService)
		{
			_eventDispatcher = eventDispatcher;
			_productService = productService;
			_navigationService = navigationService;

			Products = new ObservableCollection<ProductCellViewModel>();
		}

		public override async Task InitializeAsync()
		{
			var selected = await _productService.GetSelectedProducts();

			if (selected.Any())
				Products = new ObservableCollection<ProductCellViewModel>(selected.Select(m => new ProductCellViewModel(m)));
		}

		public async void OnAddItem()
		{
			_eventDispatcher.Subscribe(Events.ItemAdded, this, OnItemAdded);

			var hasUnselectedProducts = await _productService.HasUnselectedProducts();
			hasUnselectedProducts = true;
			if (hasUnselectedProducts)
				await _navigationService.NavigateToAsync<ProductInventoryViewModel>();
			else
				await _navigationService.NavigateToAsync<NewItemModalViewModel>(true);
		}

		private void OnItemAdded(object payload)
		{
			_eventDispatcher.Unsubscribe(Events.ItemAdded, this);

			Products.Add(new ProductCellViewModel((ProductModel)payload, true));
			RaisePropertyChanged(nameof(Products));
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(Products), nameof(HasItems));
		}
	}
}
