using FormulaOne.Entities.DbSet;

namespace FormulaOne.Data.Repositories.Interfaces;

public interface IAchievementRepository:IGenericRepository<Achievement>
{
    Task<Achievement?> GetDriverAchievementsAsync(Guid driverId);
    
}