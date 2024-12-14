using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category, int>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    Task<List<Category>> GetCategoryWithProductAsync();
}