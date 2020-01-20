using System;
using System.Threading.Tasks;
using AuctionPortal.DataAccessLayer.EntityFramework.Entities;
using AuctionPortal.Infrastructure;
using AuctionPortal.Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace AuctionPortal.DataAccessLayer.EntityFramework.Tests.RepositoryTests
{
	[TestFixture]
	public class CategoryRepositoryTests
	{
		private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

		private readonly IRepository<Category> categoryRepository = Initializer.Container.Resolve<IRepository<Category>>();

		private readonly Guid vehiclesCategoryId = Guid.Parse("aa01dc64-5c07-40fe-a916-175165b9b90f");

		private readonly Guid skodaCategoryId = Guid.Parse("aa02dc64-5c07-40fe-a916-175165b9b90f");

		[Test]
		public async Task GetCategoryAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectCategory()
		{
			Category skodaCategory;

			using (unitOfWorkProvider.Create())
			{
				skodaCategory = await categoryRepository.GetAsync(skodaCategoryId);
			}

			Assert.AreEqual(skodaCategory.Id, skodaCategoryId);
		}

		[Test]
		public async Task GetCategoryAsync_AlreadyStoredInDBWithIncludes_ReturnsCorrectCategoryWithInitializedParent()
		{
			Category skodaCategory;

			using (unitOfWorkProvider.Create())
			{
				skodaCategory = await categoryRepository.GetAsync(skodaCategoryId, nameof(Category.Parent));
			}

			Assert.IsTrue(skodaCategory.Id.Equals(skodaCategoryId) && skodaCategory.Parent.Id.Equals(vehiclesCategoryId));
		}

		[Test]
		public async Task CreateCategoryAsync_CategoryIsNotPreviouslySeeded_CreatesNewCategory()
		{
			var renaultCategory = new Category { Name = "Renault", ParentId = vehiclesCategoryId };

			using (var uow = unitOfWorkProvider.Create())
			{
				categoryRepository.Create(renaultCategory);
				await uow.Commit();

			}
			Assert.IsTrue(!renaultCategory.Id.Equals(Guid.Empty));
		}

		[Test]
		public async Task UpdateCategoryAsync_CategoryIsPreviouslySeeded_UpdatesCategory()
		{
			Category updatedSkodaCategory;
			var newSkodaCategory = new Category { Id = skodaCategoryId, Name = "Updated Name", ParentId = null };

			using (var uow = unitOfWorkProvider.Create())
			{
				categoryRepository.Update(newSkodaCategory);
				await uow.Commit();
				updatedSkodaCategory = await categoryRepository.GetAsync(skodaCategoryId);
			}

			Assert.IsTrue(newSkodaCategory.Name.Equals(updatedSkodaCategory.Name) && newSkodaCategory.ParentId.Equals(null));
		}

		[Test]
		public async Task DeleteCategoryAsync_CategoryIsPreviouslySeeded_DeletesCategory()
		{
			Category deletedSkodaCategory;

			using (var uow = unitOfWorkProvider.Create())
			{
				categoryRepository.Delete(skodaCategoryId);
				await uow.Commit();
				deletedSkodaCategory = await categoryRepository.GetAsync(skodaCategoryId);
			}

			Assert.AreEqual(deletedSkodaCategory, null);
		}
	}
}
