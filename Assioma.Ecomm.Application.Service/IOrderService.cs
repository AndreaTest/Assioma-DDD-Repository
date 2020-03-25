using System.Collections.Generic;
using System.Threading.Tasks;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Application.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAsync();
        Task<OrderViewModel> GetAsync(int id);
    }
}