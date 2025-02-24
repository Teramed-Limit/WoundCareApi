using Microsoft.EntityFrameworkCore;
using WoundCareApi.Persistence.UnitOfWork;

public class GenericUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _context;

    public GenericUnitOfWork(TContext context)
    {
        _context = context;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
