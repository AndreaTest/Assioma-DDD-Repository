using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assioma.Ecomm.Presentation.ViewModel;

namespace Assioma.Ecomm.Presentation.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new List<ProductViewModel>());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Products/Edit
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // GET: Products/Delete
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}