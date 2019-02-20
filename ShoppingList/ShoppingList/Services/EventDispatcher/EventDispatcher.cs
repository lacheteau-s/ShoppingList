using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingList.Services
{
	public class EventDispatcher : IEventDispatcher
	{
		private struct Subscriber
		{
			public Action<object> Action { get; set; }

			public WeakReference<object> Target { get; set; }
		}

		private Dictionary<string, List<Subscriber>> _subscribers = new Dictionary<string, List<Subscriber>>();

		public void Publish(string evt, object payload)
		{
			if (_subscribers.ContainsKey(evt))
			{
				var subscribers = _subscribers[evt].ToList();

				foreach (var subscriber in subscribers)
					subscriber.Action.Invoke(payload);
			}
		}

		public void Subscribe(string evt, object recipient, Action<object> action)
		{
			if (!_subscribers.ContainsKey(evt))
				_subscribers.Add(evt, new List<Subscriber>());

			if (!_subscribers[evt].Any(m => m.Target == recipient))
				_subscribers[evt].Add(new Subscriber
				{
					Action = action,
					Target = new WeakReference<object>(recipient)
				});
		}

		public void Unsubscribe(string evt, object recipient)
		{
			if (_subscribers.ContainsKey(evt))
			{
				var receivers = _subscribers[evt];
				var toRemove = receivers.Where(m => m.Target.TryGetTarget(out object obj) ? (obj == recipient) : false).ToList();

				foreach (var obj in toRemove)
					receivers.Remove(obj);
			}
		}
	}
}
