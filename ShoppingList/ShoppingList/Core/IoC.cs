using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Core
{
	public class IoC
	{
		private static readonly object _lock = new object();
		private static volatile Container _container;
		private static Container _safeContainer => GetSafeContainer();

		private static Container GetSafeContainer()
		{
			if (_container != null)
				return _container;

			lock (_lock)
			{
				if (_container == null)
				{
					_container = new Container();
					_container.ResolveUnregisteredType += (sender, e) => throw new ArgumentException($"{e.UnregisteredServiceType} is not registered in the IoC container");
				}
			}

			return _container;
		}

		public static void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			_safeContainer.Register<TInterface, TImplementation>();
		}

		public static void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			_safeContainer.RegisterSingleton<TInterface, TImplementation>();
		}
	}
}
