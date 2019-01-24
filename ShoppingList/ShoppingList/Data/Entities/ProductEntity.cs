using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Data.Entities
{
	[Table("Product")]
	public class ProductEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public bool Selected { get; set; }
	}
}
