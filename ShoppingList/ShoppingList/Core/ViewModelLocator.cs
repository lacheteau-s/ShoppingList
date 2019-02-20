using ShoppingList.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ShoppingList.Core
{
	public static class ViewModelLocator
	{
		public static readonly BindableProperty AutoWireViewModelProperty =
			BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

		public static bool GetAutoWireViewModel(BindableObject bindable)
		{
			return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
		}

		public static void SetAutoWireViewModel(BindableObject bindable, bool value)
		{
			bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
		}

		private static async void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as Element;

			if (view == null)
				return;

			var viewType = view.GetType();
			var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
			var viewAssembly = viewType.GetTypeInfo().Assembly.FullName;
			var viewModelName = $"{viewName}Model, {viewAssembly}";
			var viewModelType = Type.GetType(viewModelName);

			if (viewModelType == null)
				return;

			var viewModel = (BaseViewModel)IoC.GetInstance(viewModelType);
			await viewModel.InitializeAsync(); // TODO: sharpnado initialize async + rm BaseViewModel
			view.BindingContext = viewModel;
		}
	}
}
