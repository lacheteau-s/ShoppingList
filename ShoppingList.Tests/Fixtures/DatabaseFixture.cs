using ShoppingList.Services;
using ShoppingList.Tests.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingList.Tests.Fixtures
{
	public class DatabaseFixture : IAsyncLifetime
	{
		private readonly string _path = Path.Combine(Environment.CurrentDirectory, "dbFixture.db3");

		public IDatabaseService DatabaseService { get; private set; }
		public DummyEntity DummyEntity { get; private set; }

		public DatabaseFixture()
		{
			DatabaseService = new DatabaseService();
			DummyEntity = new DummyEntity { Name = "Dummy" };
		}

		public async Task InitializeAsync()
		{
			await DatabaseService.CreateConnectionAsync(_path);
			await DatabaseService.CreateTableAsync<DummyEntity>();
		}

		public async Task DisposeAsync()
		{
			await DatabaseService.CloseConnectionAsync();

			File.Delete(_path);
		}
	}
}
