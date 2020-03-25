using Microsoft.EntityFrameworkCore;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Domain.InfrastructureContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assioma.Ecomm.Data
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        #region PRIVATE FIELDS
        private readonly DbContext unitOfWork;
        #endregion

        #region CTOR
        /// <summary>
        /// Create a new instance
        /// </summary>
        public OrderRepository(DbContext dbcontext) : base(dbcontext)
        {
            if (dbcontext == null) { throw new ArgumentNullException("dbcontext"); }
            this.unitOfWork = dbcontext;
        }
        #endregion

        public virtual async Task<Order> GetFullOrderAsync(int id)
        {
            return await ((IQueryable<Order>)DbQuery)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
