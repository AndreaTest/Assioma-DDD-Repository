using System.Collections.Generic;

namespace Assioma.Ecomm.Domain
{
    public class Product : Entity<int>
    {
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; protected set; }

        public decimal Price { get; protected set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; protected set; }

        #region CTOR
        public Product() { } // EF
        #endregion
    }
}
