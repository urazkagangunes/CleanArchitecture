namespace App.Repositories;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => appDbContext.SaveChangesAsync();

}