using ShoppingList.Core;
using ShoppingList.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingList.ViewModels
{
    public class HomeViewModel
    {
		private readonly IEventDispatcher _eventDispatcher;

		public ObservableCollection<ProductViewModel> Products { get; set; }

		public HomeViewModel(IEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
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
		}
    }
}
