using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assioma.Ecomm.Presentation.ViewModel;
using Microsoft.Extensions.Logging;

namespace Assioma.Ecomm.Presentation.Web.Controllers.api
{
    [Route("api/order")]
    public class OrderAPIController : Controller
    {
        private readonly ILogger<OrderAPIController> _logger;

        public OrderAPIController(ILogger<OrderAPIController> logger)
        {
            _logger = logger;
        }
    }
}
