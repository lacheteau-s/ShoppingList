using ShoppingList.Core;
using ShoppingList.Data;
using ShoppingList.Data.Entities;
using ShoppingList.Services;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
			InitializeNavigation();

			MainPage = new NavigationPage(IoC.GetInstance<HomeView>());
		}

		private void InitializeIoC()
		{
			IoC.Register<HomeView>();
			IoC.Register<HomeViewModel>();
			IoC.Register<NewItemModalView>();
			IoC.Register<NewItemModalViewModel>();
			IoC.Register<ProductInventoryView>();
			IoC.Register<ProductInventoryViewModel>();
			IoC.RegisterSingleton<IDatabaseService, DatabaseService>();
			IoC.RegisterSingleton<IProductService, ProductService>();
			IoC.RegisterSingleton<IAsyncRepository<ProductEntity>, AsyncRepository<ProductEntity>>();
			IoC.RegisterSingleton<IEventDispatcher, EventDispatcher>();
			IoC.RegisterSingleton<INavigationService, NavigationService>();
		}

		private void InitializeNavigation()
		{
			var navigationService = IoC.GetInstance<INavigationService>();

			navigationService.Map<NewItemModalViewModel, NewItemModalView>();
			navigationService.Map<ProductInventoryViewModel, ProductInventoryView>();
		}

		protected override async void OnSleep()
		{
			var dbService = IoC.GetInstance<IDatabaseService>();

			await dbService.CloseConnectionAsync();
		}

		protected override void OnResume()
		{
			var dbService = IoC.GetInstance<IDatabaseService>();

			dbService.CreateConnectionAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shoppingList.db3"));
		}

		private void InitializeDatabase()
		{
			var dbService = IoC.GetInstance<IDatabaseService>();

			dbService.CreateConnectionAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shoppingList.db3")).Wait();
			var table = dbService.DbContext?.TableMappings.SingleOrDefault(t => t.TableName == "Product");

			if (table == null)
				dbService.CreateTableAsync<ProductEntity>().Wait();
		}
	}
}
