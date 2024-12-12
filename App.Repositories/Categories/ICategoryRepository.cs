namespace App.Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category, int>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    IQueryable<Category> GetCategoryWithProduct();
}