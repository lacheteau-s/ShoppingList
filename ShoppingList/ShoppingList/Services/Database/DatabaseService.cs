using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public class DatabaseService : IDatabaseService
	{
		private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

		public SQLiteAsyncConnection DbContext { get; private set; }

		public async Task CreateConnectionAsync(string path)
		{
			await semaphore.WaitAsync();

			try
			{
				if (DbContext == null)
					DbContext = new SQLiteAsyncConnection(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
			}
			finally
			{
				semaphore.Release();
			}
		}

		public async Task CloseConnectionAsync()
		{
			await semaphore.WaitAsync();

			try
			{
				if (DbContext != null)
				{
					await DbContext.CloseAsync();
					DbContext.GetConnection().Dispose();
					DbContext = null;
				}
			}
			finally
			{
				semaphore.Release();
			}
		}

		public Task CreateTableAsync<T>() where T : class, new()
		{
			return DbContext.CreateTableAsync<T>();
		}
	}
}
