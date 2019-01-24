using ShoppingList.Data.Entities;
using ShoppingList.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingList.Tests
{
	public class DatabaseTests
	{
		private readonly IDatabaseService _dbService = new DatabaseService();
		private readonly string _dbPath = Path.Combine(Environment.CurrentDirectory, "testDb.db3");

		[Fact]
		public void CreateConnection_Success()
		{
			CreateConnection();

			Assert.True(File.Exists(_dbPath));
			// TODO: close (https://github.com/praeclarum/sqlite-net/issues/740)
		}

		[Fact]
		public async Task CreateTable_Success()
		{
			CreateConnection();

			await _dbService.CreateTableAsync<ProductEntity>(); // TODO: Mock/Project agnostic entity

			var table = _dbService.DbContext.TableMappings.SingleOrDefault(t => t.TableName == "Product");

			Assert.NotNull(table);

			//File.Delete(_dbPath); // TODO
		}

		private void CreateConnection()
		{
			// TODO
			//if (File.Exists(_dbPath))
			//	File.Delete(_dbPath);

			_dbService.CreateConnection(_dbPath);
		}
	}
}
