using ShoppingList.Services;
using ShoppingList.Tests.Data;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingList.Tests
{
	[TestCaseOrderer("ShoppingList.Tests.PriorityOrderer", "ShoppingList.Tests")]
	public class DatabaseTests
	{
		private static IDatabaseService _dbService = new DatabaseService();
		private readonly string _dbPath = Path.Combine(Environment.CurrentDirectory, "dbTest.db3");

		[Fact, Priority(0)]
		public async Task CreateConnection_Success()
		{
			if (File.Exists(_dbPath))
				File.Delete(_dbPath);

			await _dbService.CreateConnectionAsync(_dbPath);

			// Database file won't be created until the connection isn't being used.
			var conn = _dbService.DbContext.GetConnection();

			Assert.NotNull(conn);
			Assert.True(File.Exists(_dbPath));
		}

		[Fact, Priority(1)]
		public async Task CreateTable_Success()
		{
			await _dbService.CreateTableAsync<DummyEntity>();

			var table = _dbService.DbContext
								  .TableMappings
								  .SingleOrDefault(t => t.TableName == "Dummy");

			Assert.NotNull(table);
		}

		[Fact, Priority(2)]
		public async Task CloseConnection_Success()
		{
			await _dbService.CloseConnectionAsync();

			Assert.Null(_dbService.DbContext);

			File.Delete(_dbPath);

			Assert.False(File.Exists(_dbPath));
		}
	}
}
