using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public static void Register<T>() where T : class
		{
			_safeContainer.Register<T>();
		}

		public static void Register<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			_safeContainer.Register<TInterface, TImplementation>();
		}

		public static void RegisterSingleton<T>() where T : class
		{
			_safeContainer.RegisterSingleton<T>();
		}

		public static void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
		{
			_safeContainer.RegisterSingleton<TInterface, TImplementation>();
		}

		public static T GetInstance<T>(bool throwIfUnregistered = true) where T : class
		{
			if (throwIfUnregistered)
				return _safeContainer.GetInstance<T>();

			return IsRegistered<T>() ? _safeContainer.GetInstance<T>() : null;
		}

		public static bool IsRegistered<T>() where T : class
		{
			return _safeContainer.GetCurrentRegistrations().Count(p => p.ServiceType == typeof(T)) > 0;
		}
	}
}
