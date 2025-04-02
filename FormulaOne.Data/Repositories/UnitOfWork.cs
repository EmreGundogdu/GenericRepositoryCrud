using FormulaOne.Data.Data;
using FormulaOne.Data.Repositories.Interfaces;

namespace FormulaOne.Data.Repositories;

public class UnitOfWork:IUnitOfWork,IDisposable, IAsyncDisposable
{
    private readonly AppDbContext _dbContext;
    public IDriverRepository Drivers { get; }
    public IAchievementRepository Achievements { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CompleteAsync()
    {
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }


    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}