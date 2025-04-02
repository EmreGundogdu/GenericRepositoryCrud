using FormulaOne.Data.Data;
using FormulaOne.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Data.Repositories;

public class GenericRepository<T>  : IGenericRepository<T> where T : class
{
    protected AppDbContext _dbContext;
    internal DbSet<T> _dbSet;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext; 
        _dbSet = _dbContext.Set<T>();
    }
    
    public virtual  IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return  await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Add(T entity)
    {
        return await _dbSet.AddAsync(entity) != null;
    }

    public virtual async Task<bool> Update(T entity)
    {
        return _dbSet.Update(entity) != null;
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        return _dbSet.Remove(await _dbSet.FindAsync(id)) != null;
    }
}