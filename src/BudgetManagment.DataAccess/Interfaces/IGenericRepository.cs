using BudgetManagment.Domain.Commons;
using System.Linq.Expressions;

namespace BudgetManagment.DataAccess.Interfaces;

public interface IGenericRepository<TSource> 
{
   public ValueTask<TSource> InsertAsync(TSource entity);
   public ValueTask<TSource> UpdateAsync(TSource entity);
   public ValueTask<bool> DeleteAsync(TSource entity);
   public ValueTask SaveAsync();
   public IQueryable<TSource> SelectAll(
        Expression<Func<TSource, bool>> expression = null, string[] includes = null);
   public ValueTask<TSource> SelectAsync(Expression<Func<TSource, bool>> expression, string[] includes = null);
}


