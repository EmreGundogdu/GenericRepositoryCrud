using FormulaOne.Data.Data;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Data.Repositories;

public class DriverRepository:GenericRepository<Driver>,IDriverRepository
{
    public DriverRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }

    public override IQueryable<Driver> GetAll()
    {
        return  _dbSet.Where(x => x.Status == 1).AsNoTracking().AsSplitQuery().OrderBy(x => x.CreatedDate)
            .AsQueryable();
    }

    public override async Task<bool> Delete(Guid id)
    {
        var result =  await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            return false;

        result.Status = 0;
        result.UpdatedDate = DateTime.Now;
        return true;
    }
    
    public override async Task<bool> Update(Driver driver)
    {
        var result =  await _dbSet.FirstOrDefaultAsync(x => x.Id == driver.Id);
        if (result == null)
            return false;

        result.UpdatedDate = DateTime.Now;
        result.DriverNumber = driver.DriverNumber;
        result.FirstName = driver.FirstName;
        result.LastName = driver.LastName;
        result.DateOfBirth = driver.DateOfBirth;
        return true;
    }
}