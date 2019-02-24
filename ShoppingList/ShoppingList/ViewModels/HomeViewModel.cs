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

		public ICommand AddNewItemCommand => new Command(OnAddNewItem);

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

		public async void OnAddNewItem()
		{
			_eventDispatcher.Subscribe(Events.ItemAdded, this, OnNewItemAdded);

			await _navigationService.NavigateToAsync<NewItemModalViewModel>(true);
		}

		private void OnNewItemAdded(object payload)
		{
			_eventDispatcher.Unsubscribe(Events.ItemAdded, this);

			Products.Add(new ProductCellViewModel((ProductModel)payload));
			RaisePropertyChanged(nameof(Products));
		}

		protected override void RegisterDependencies()
		{
			RegisterDependency(nameof(Products), nameof(HasItems));
		}
	}
}
