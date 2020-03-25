using System.Threading.Tasks;

namespace Assioma.Ecomm.Domain.InfrastructureContracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetFullOrderAsync(int id);
    }
}
