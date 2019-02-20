using ShoppingList.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Data
{
	public class AsyncRepository<T> : IAsyncRepository<T> where T : class, new()
	{
		private readonly IDatabaseService _dbService;

		private SQLiteAsyncConnection _dbContext => _dbService.DbContext;

		public AsyncRepository(IDatabaseService databaseService)
		{
			_dbService = databaseService;
		}

		public Task DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAsync(int id)
		{
			return _dbContext.FindAsync<T>(id);
		}

		public Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null) // TODO: OrderBy
		{
			var query = _dbContext.Table<T>();

			if (predicate != null)
				query = query.Where(predicate);

			return query.ToListAsync();
		}

		public Task<int> InsertAsync(T entity)
		{
			return _dbContext.InsertAsync(entity);
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
