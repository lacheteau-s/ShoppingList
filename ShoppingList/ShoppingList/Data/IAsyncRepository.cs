using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Data
{
	public interface IAsyncRepository<T> where T : class, new()
	{
		AsyncTableQuery<T> AsQueryable();
		Task<IEnumerable<T>> GetAsync();
		Task<T> GetAsync(int id);
		Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null);
		Task<int> InsertAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
