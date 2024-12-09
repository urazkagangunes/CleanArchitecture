namespace App.Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    IQueryable<Category> GetCategoryByProductAsync();
}