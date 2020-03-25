using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assioma.Ecomm.Presentation.ViewModel;
using Microsoft.Extensions.Logging;
using Assioma.Ecomm.Application.Service;
using Newtonsoft.Json;

namespace Assioma.Ecomm.Presentation.Web.Controllers.api
{
    [Route("api/product")]
    public class ProductAPIController : Controller
    {
        private readonly ILogger<ProductAPIController> _logger;
        private readonly IProductService _productService;

        public ProductAPIController(
            ILogger<ProductAPIController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            _logger.LogInformation($"{DateTime.Now}: product list retrieved.");
            return Json(await _productService.GetAsync());
        }

        // GET: api/product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(int id)
        {
            _logger.LogInformation($"{DateTime.Now}: product {id} retrieved.");
            return Json(await _productService.GetAsync(id));
        }

        // POST: api/product
        [HttpPost]
        public async Task Post([FromForm] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    await _productService.CreateAsync(product);
                    _logger.LogInformation($"{DateTime.Now}: new product created.");
                }
                else
                {
                    await _productService.ModifyAsync(product);
                    _logger.LogInformation($"{DateTime.Now}: new product {product.Id} updated.");
                }
            }
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.DeleteAsync(id);
            _logger.LogInformation($"{DateTime.Now}: product {id} deleted.");
        }
    }
}
