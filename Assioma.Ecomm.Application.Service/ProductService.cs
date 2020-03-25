using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assioma.Ecomm.Domain;
using Assioma.Ecomm.Domain.InfrastructureContracts;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            ILogger<ProductService> logger,
            IRepository<Product> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets all the products.
        /// </summary>
        public async Task<IEnumerable<ProductViewModel>> GetAsync()
        {
            List<Product> products = null;

            try
            {
                products = await _productRepository.Get(p => p.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                // In a real application I would manage the error sending back to the client a feedback.
                throw;
            }

            if (products == null)
            {
                _logger.LogInformation($"{DateTime.Now}: There are no products to retrieve.");
                products = new List<Product>();
            }

            return products.Select(p => Mapper.Map<ProductViewModel>(p));
        }

        /// <summary>
        /// Given the product id, it gets it.
        /// </summary>
        public async Task<ProductViewModel> GetAsync(int id)
        {
            Product product = null;

            try
            {
                product = await _productRepository.Get(id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                // In a real application I would manage the error sending back to the client a feedback.
                throw;
            }

            if (product == null)
            {
                _logger.LogError($"{DateTime.Now}: There products with the id = {id} does not exist.");

                // In a real application I would manage the error sending back to the client a feedback.
                throw new Exception("The product does not exist.");
            }

            return Mapper.Map<ProductViewModel>(product);
        }

        /// <summary>
        /// Given a new product it creates it.
        /// </summary>
        public async Task<ProductViewModel> CreateAsync(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                _logger.LogError($"{DateTime.Now}: There products is not a valid product.");

                // In a real application I would manage the error sending back to the client a feedback.
                throw new Exception("The product does not exist.");
            }

            try
            {
                _productRepository.Add(Mapper.Map<Product>(productViewModel));
                await _productRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                // In a real application I would manage the error sending back to the client a feedback.
                throw;
            }

            return productViewModel;
        }

        /// <summary>
        /// Given the product it saves it.
        /// </summary>
        public async Task<ProductViewModel> ModifyAsync(ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                _logger.LogError($"{DateTime.Now}: There products is not a valid product.");

                // In a real application I would manage the error sending back to the client a feedback.
                throw new Exception("The product does not exist.");
            }

            try
            {
                _productRepository.Modify(Mapper.Map<Product>(productViewModel));
                await _productRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                // In a real application I would manage the error sending back to the client a feedback.
                throw;
            }

            return productViewModel;
        }

        /// <summary>
        /// Given the product it deletes it.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            Product product = null;

            try
            {
                product = _productRepository.Get(id).FirstOrDefault();
                _productRepository.Remove(product);
                await _productRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                // In a real application I would manage the error sending back to the client a feedback.
                throw;
            }
        }
    }
}
