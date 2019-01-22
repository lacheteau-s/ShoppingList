using ShoppingList.Core;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShoppingList
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			InitializeIoC();

			MainPage = IoC.GetInstance<HomePage>();
		}

		private void InitializeIoC()
		{
			IoC.Register<HomePage>();
			IoC.Register<HomeViewModel>();
			IoC.Register<NewItemModalPage>();
			IoC.Register<NewItemModalViewModel>();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
