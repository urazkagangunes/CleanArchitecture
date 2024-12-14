using App.Application.Contracts.Persistence;

namespace App.Persistence;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => appDbContext.SaveChangesAsync();

}