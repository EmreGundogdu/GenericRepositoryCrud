namespace FormulaOne.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    IDriverRepository Drivers { get; }
    IAchievementRepository Achievements { get; }
    Task<bool> CompleteAsync();
}