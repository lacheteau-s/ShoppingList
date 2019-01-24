using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
	public class DatabaseService : IDatabaseService
	{
		private readonly object _lock = new object();

		public SQLiteAsyncConnection DbContext { get; private set; }

		public void CreateConnection(string path)
		{
			lock (_lock)
			{
				if (DbContext == null)
					DbContext = new SQLiteAsyncConnection(path);
			}
		}

		public Task CreateTableAsync<T>() where T : class, new()
		{
			return DbContext.CreateTableAsync<T>();
		}
	}
}
