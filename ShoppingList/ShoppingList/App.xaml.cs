using ShoppingList.Core;
using ShoppingList.Data.Entities;
using ShoppingList.Services;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using System;
using System.IO;
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
			InitializeDatabase();

			MainPage = IoC.GetInstance<HomeView>();
		}

		private void InitializeIoC()
		{
			IoC.Register<HomeView>();
			IoC.Register<HomeViewModel>();
			IoC.Register<NewItemModalView>();
			IoC.Register<NewItemModalViewModel>();
			IoC.RegisterSingleton<IDatabaseService, DatabaseService>();
		}

		private void InitializeDatabase()
		{
			var dbService = IoC.GetInstance<IDatabaseService>();

			dbService.CreateConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shoppingList.db3"));
			dbService.CreateTableAsync<ProductEntity>();
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
