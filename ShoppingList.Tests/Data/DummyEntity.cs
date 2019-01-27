using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Tests.Data
{
	[Table("Dummy")]
	public class DummyEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
