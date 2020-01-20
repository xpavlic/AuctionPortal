using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionPortal.BusinessLayer.DataTransferObjects;
using AuctionPortal.BusinessLayer.DataTransferObjects.Common;
using AuctionPortal.BusinessLayer.DataTransferObjects.Filters;

namespace AuctionPortal.BusinessLayer.Services.Categories
{
    public interface ICategoryService
    {
        /// <summary>
        /// Gets ids of the categories with the corresponding names
        /// </summary>
        /// <param name="names">names of the categories</param>
        /// <returns>ids of categories with specified name</returns>
        Task<Guid[]> GetCategoryIdsByNamesAsync(params string[] names);

        /// <summary>
        /// Gets all parent categories for specified category
        /// </summary>
        /// <param name="categoryId">category id</param>
        /// <returns>all parent categories</returns>
        Task<IEnumerable<CategoryDTO>> GetCategoryPathAsync(Guid categoryId);

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<CategoryDTO> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(CategoryDTO entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(CategoryDTO entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<CategoryDTO, CategoryFilterDto>> ListAllAsync();
    }
}
