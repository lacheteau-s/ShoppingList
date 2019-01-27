using ShoppingList.Data;
using ShoppingList.Services;
using ShoppingList.Tests.Data;
using ShoppingList.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingList.Tests
{
	public class AsyncRepositoryTests : IClassFixture<DatabaseFixture>
	{
		private readonly DatabaseFixture _fixture;
		private static IAsyncRepository<DummyEntity> _repository;

		public AsyncRepositoryTests(DatabaseFixture fixture)
		{
			_fixture = fixture;
			_repository = new AsyncRepository<DummyEntity>(_fixture.DatabaseService);
		}

		[Fact, Priority(0)]
		public async Task InsertAsync_ReturnsId()
		{
			var id = await _repository.InsertAsync(_fixture.DummyEntity);

			Assert.Equal(1, id);
		}

		[Fact, Priority(1)]
		public async Task GetByIdAsync_WithValidId_ReturnsModel()
		{
			var id = await _repository.InsertAsync(_fixture.DummyEntity);
			var model = await _repository.GetAsync(id);

			Assert.NotNull(model);
		}

		[Fact]
		public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
		{
			var model = await _repository.GetAsync(42);

			Assert.Null(model);
		}
	}
}
