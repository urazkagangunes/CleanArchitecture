using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence;

public class GenericRepository<T, TId>(AppDbContext appDbContext)
    : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    protected AppDbContext Context = appDbContext;

    private readonly DbSet<T> _dbSet = appDbContext.Set<T>();

    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));
    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    //public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);

    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
}
