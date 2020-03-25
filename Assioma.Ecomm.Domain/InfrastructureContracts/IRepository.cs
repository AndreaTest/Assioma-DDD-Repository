    using System;
    using System.Linq;
    using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assioma.Ecomm.Domain.InfrastructureContracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<int>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get<TKProperty>(Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending = true);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Get<TKProperty>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending = true);
        IQueryable<TEntity> Get(int id);
        IQueryable<TEntity> Get(int id, params Expression<Func<TEntity, object>>[] includes);
        void Add(TEntity item);
        void Modify(TEntity item);
        void Remove(TEntity item);
        Task SaveAsync();
    }
}
