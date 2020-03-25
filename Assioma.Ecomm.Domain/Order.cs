    using System;
    using System.Collections.Generic;

namespace Assioma.Ecomm.Domain
{
    public class Order : Entity<int>
    {
        public Order(DateTime orderDate)
        {
            OrderDate = orderDate;
        }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public DateTime OrderDate { get; protected set; }

        #region CTOR
        public Order() { } // EF
        #endregion
    }
}
