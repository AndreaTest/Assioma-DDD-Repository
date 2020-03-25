using AutoMapper;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Presentation.Automapper
{
    public class AutomapperViewModel : Profile
    {
        public AutomapperViewModel()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
        }
    }
}
