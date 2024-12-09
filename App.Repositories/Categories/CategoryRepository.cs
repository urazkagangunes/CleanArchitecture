using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _context = appDbContext;
    }

    public IQueryable<Category> GetCategoryByProductAsync()
    {
        return _context.Categories.Include(x => x.Products).AsQueryable();
    }

    public Task<Category?> GetCategoryWithProductAsync(int id)
    {
        return _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
}