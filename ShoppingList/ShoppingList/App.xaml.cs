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

			MainPage = new HomePage();
		}

		private void InitializeIoC()
		{
			// Add registrations here
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
