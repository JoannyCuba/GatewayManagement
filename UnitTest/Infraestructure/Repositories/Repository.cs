using GatewayManagementCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Infraestructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter == null)
                return query.CountAsync();
            return query.Where(filter).CountAsync();
        }
        public virtual Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            return query.FirstOrDefaultAsync(filter);
        }

        public virtual Task<List<TEntity>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            return query.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToListAsync();
        }
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual async Task<bool> AddIdentity(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }
        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        public async virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;
            var entity = await query.FirstOrDefaultAsync(filter);
            dbSet.Update(entity);
        }
    }
}
