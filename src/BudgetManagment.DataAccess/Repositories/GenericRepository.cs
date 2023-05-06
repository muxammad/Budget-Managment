using BudgetManagment.DataAccess.Context;
using BudgetManagment.DataAccess.Interfaces;
using BudgetManagment.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace BudgetManagment.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Auditable
    {
        private readonly BmDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;
        public GenericRepository(BmDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }
        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteAsync(TEntity entity)
        {
            var existEntity = await this.dbSet.FirstOrDefaultAsync(t => t.Id.Equals(entity.Id));
            if (existEntity is null) return false;
            return true;
        }
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        => (await this.dbSet.AddAsync(entity)).Entity;
        public async ValueTask SaveChangesAsync()
        => await dbContext.SaveChangesAsync();

        /// <summary>
        /// To Select All
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<TEntity> SelectAll(
         Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
        {
            IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);
            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }
        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> SelectAsync(
         Expression<Func<TEntity, bool>> expression, string[] includes = null)
         => await this.SelectAll(expression, includes).FirstOrDefaultAsync();

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
         => (this.dbSet.Update(entity)).Entity;

    }
}
