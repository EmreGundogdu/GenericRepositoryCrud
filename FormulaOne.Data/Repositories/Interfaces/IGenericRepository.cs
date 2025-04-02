namespace FormulaOne.Data.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
 IQueryable<T> GetAll();
 Task<T> GetById(Guid id);
 Task<bool> Add(T entity);
 Task<bool> Update(T entity);
 Task<bool> Delete(Guid id);

}