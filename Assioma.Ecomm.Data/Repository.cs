using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Domain.InfrastructureContracts;

namespace Assioma.Ecomm.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<int>
    {
        #region MEMBERS
        private readonly DbContext _unitOfWork;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region CTOR
        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="_unitOfWork">Associated Unit Of Work</param>
        public Repository(DbContext dbcontext)//IQueryableUnitOfWork _unitOfWork)
        {
            if (dbcontext == null)
            {
                throw new ArgumentNullException("dbcontext");
            }

            _unitOfWork = dbcontext;
            _dbSet = _unitOfWork.Set<TEntity>();
        }
        #endregion

        #region PROTECTED PROPERTIES
        protected IQueryable<TEntity> DbQuery
        {
            get
            {
                return this._dbSet.AsQueryable();
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Returns all the data.
        /// Returns IQueryable back to the application service layer.
        /// </summary>
        public virtual IQueryable<TEntity> Get()
        {
            return DbQuery.AsNoTracking();
        }

        public virtual IQueryable<TEntity> Get<TKProperty>(
            Expression<Func<TEntity, TKProperty>> orderByExpression,
            bool ascending = true)
        {
            if (ascending)
            {
                return Get().OrderBy(orderByExpression);
            }
            else
            {
                return Get().OrderByDescending(orderByExpression);
            }
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter)
        {
            return DbQuery.AsNoTracking().Where(filter);
        }

        public virtual IQueryable<TEntity> Get<TKProperty>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKProperty>> orderByExpression,
            bool ascending = true)
        {
            if (ascending)
            {
                return Get(filter).OrderBy(orderByExpression);
            }
            else
            {
                return Get(filter).OrderByDescending(orderByExpression);
            }
        }

        /// <summary>
        /// Returns a single record.
        /// Returns IQueryable back to the application service layer.
        /// </summary>
        public virtual IQueryable<TEntity> Get(int id)
        {
            return DbQuery.AsNoTracking().Where(entity => entity.Id == id);
        }

        /// <summary>
        /// Returns a single record.
        /// This works on EF6 System.Data.Entity.QueryableExtensions
        /// but seems not working on EF Core 2 - Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions: double check!
        /// It does not work with nested includes like: inc => inc.Products.Select(p => p.SomethingEleseNested);
        /// Returns IQueryable back to the application service layer.
        /// </summary>
        public virtual IQueryable<TEntity> Get(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            if (id != default(int))
            {
                IQueryable<TEntity> dbqry = DbQuery;
                dbqry = includes.Aggregate(dbqry,
                          (current, include) => current.Include<TEntity, object>(include));
                return dbqry.AsNoTracking().Where(entity => entity.Id == id);
            }
            else
            {
                return null;
            }
        }

        public virtual void Add(TEntity item)
        {

            if (item != (TEntity)null)
            {
                try
                {
                    this._dbSet.Add(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                // Omitted log for semplicity
                throw new Exception("The Entity to add is null.");
            }

        }

        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _unitOfWork.Entry<TEntity>(item).State = EntityState.Modified;
            }
            else
            {
                // Omitted for semplicity
                // ... LogInfo(ErrorMessages.Info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        public virtual void Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                // Set as deleted
                _unitOfWork.Entry<TEntity>(item).State = EntityState.Deleted;
            }
            else
            {
                // Omitted for semplicity
                // ... LoggerFactory.CreateLog().LogInfo(ErrorMessages.Info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Repository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void System.IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
