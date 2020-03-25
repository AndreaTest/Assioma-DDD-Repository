using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Domain.InfrastructureContracts;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly ILogger<OrderService> _logger;

        public OrderService(
           //ILogger<OrderService> logger,
           IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Given the order id, it gets it.
        /// </summary>
        public async Task<IEnumerable<OrderViewModel>> GetAsync()
        {
            var orders = await _orderRepository.Get().ToListAsync();

            if (orders == null)
            {
                throw new Exception("The order does not exist.");
            }

            return orders.Select(p => Mapper.Map<OrderViewModel>(p));
        }

        /// <summary>
        /// Given the order id, it gets it.
        /// </summary>
        public async Task<OrderViewModel> GetAsync(int id)
        {
            // This works on EF6 System.Data.Entity.QueryableExtensions
            // but seems not working on EF Core 2 - Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions: double check!
            // With something like that:
            // var order = _orderRepository.Get(id, inc => inc.Products, inc => inc.Products.Select(p => p.SomethingEleseNested));

            // Work around: to use a custom repository
            var order = await _orderRepository.GetFullOrderAsync(id);

            if (order == null)
            {
                throw new Exception("The order does not exist.");
            }

            return Mapper.Map<OrderViewModel>(order);
        }
    }
}
