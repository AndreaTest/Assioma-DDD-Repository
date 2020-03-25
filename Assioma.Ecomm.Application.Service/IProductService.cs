using System.Collections.Generic;
using System.Threading.Tasks;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Application.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAsync();
        Task<ProductViewModel> GetAsync(int id);
        Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel);
        Task<ProductViewModel> ModifyAsync(ProductViewModel productViewModel);
        Task DeleteAsync(int id);
    }
}