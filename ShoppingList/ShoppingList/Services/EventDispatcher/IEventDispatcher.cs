using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Services
{
    public interface IEventDispatcher
    {
		void Publish(string evt, object payload);

		void Subscribe(string evt, object recipient, Action<object> action);

		void Unsubscribe(string evt, object recipient);
    }
}
