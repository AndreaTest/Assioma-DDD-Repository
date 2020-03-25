using System;
using System.Collections.Generic;
using System.Text;
using Assioma.Ecomm.Domain;

namespace Assioma.Ecomm.Presentation.ViewModel
{
    public class OrderViewModel
    {
        public virtual int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
