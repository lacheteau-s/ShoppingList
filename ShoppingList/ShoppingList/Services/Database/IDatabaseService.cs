using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public interface IDatabaseService
	{
		SQLiteAsyncConnection DbContext { get; }

		Task CreateConnectionAsync(string path);
		Task CloseConnectionAsync();
		Task CreateTableAsync<T>() where T : class, new();
	}
}
