using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Data
{
	public interface IAsyncRepository<T> where T : class, new()
	{
		Task<IEnumerable<T>> GetAsync();
		Task<T> GetAsync(int id);
		Task<int> InsertAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
