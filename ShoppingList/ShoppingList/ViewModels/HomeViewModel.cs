using ShoppingList.Core;
using ShoppingList.Models;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
		private readonly IEventDispatcher _eventDispatcher;

		private bool _isEmpty;
		private bool _hasItems;

		public bool IsEmpty
		{
			get { return _isEmpty; }
			set { SetProperty(ref _isEmpty, value); }
		}

		public bool HasItems
		{
			get { return _hasItems; }
			set { SetProperty(ref _hasItems, value); }
		}

		public ObservableCollection<ProductViewModel> Products { get; set; }

		public HomeViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
			Products = new ObservableCollection<ProductViewModel>();
			IsEmpty = true;
			HasItems = false;
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

			IsEmpty = false;
			HasItems = true;
		}
    }
}
