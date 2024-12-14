using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _context = appDbContext;
    }

    public IQueryable<Category> GetCategoryWithProduct()
    {
        return _context.Categories.Include(x => x.Products).AsQueryable();
    }

    public Task<Category?> GetCategoryWithProductAsync(int id)
    {
        return _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Category>> GetCategoryWithProductAsync()
    {
        return _context.Categories.Include(x => x.Products).ToListAsync();
    }
}