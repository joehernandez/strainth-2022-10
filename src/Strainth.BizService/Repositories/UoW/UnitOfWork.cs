namespace Strainth.BizService.Repositories.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly StrainthContext _context;

    public UnitOfWork(StrainthContext context)
    {
        _context = context;
    }

    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}