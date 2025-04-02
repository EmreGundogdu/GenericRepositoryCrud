using FormulaOne.Data.Data;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Data.Repositories;

public class AchievementRepository:GenericRepository<Achievement>,IAchievementRepository
{
    public AchievementRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async  Task<Achievement?> GetDriverAchievementsAsync(Guid driverId)
    {
        return await _dbContext.Achievements.FirstOrDefaultAsync(x => x.DriverId == driverId);
    }
    
    public override IQueryable<Achievement> GetAll()
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
    
    public override async Task<bool> Update(Achievement achievement)
    {
        var result =  await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);
        if (result == null)
            return false;

        result.UpdatedDate = DateTime.Now;
        result.FastestLap = achievement.FastestLap;
        result.PolePosition = achievement.PolePosition;
        result.RaceWins = achievement.RaceWins;
        result.WorlChampionship = achievement.WorlChampionship;
        return true;
    }
}