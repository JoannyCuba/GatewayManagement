using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GatewayManagementCore.Interfaces
{
    public interface IRepository<T>
    {
        public Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        public Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);
        public Task<List<T>> FindPaginationAsync(int page = 1, int itemPerPage = 25, Expression<Func<T, bool>>? filter = null);
        public Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
        public void Add(T entity);
        public Task<bool> AddIdentity(T entity);
        public void Update(T entity);
        public void Remove(T entity);
        public void Delete(Expression<Func<T, bool>> filter);
    }
}
