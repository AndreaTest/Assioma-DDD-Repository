using System;
using System.Collections.Generic;

namespace Assioma.Ecomm.Domain
{
    public class OrderProduct : Entity<int>
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; } // In lack of better name.
        public virtual Product Product { get; set; }

        #region CTOR
        public OrderProduct() { } // EF
        #endregion
    }
}
