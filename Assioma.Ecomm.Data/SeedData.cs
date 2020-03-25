using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assioma.Ecomm.Domain;

namespace Assioma.Ecomm.Data
{
    public static class SeedData
    {
        private static EcommContext _context;

        public static void Initialize(EcommContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            // In memory we do not need async
            if (_context.Orders.Any())
            {
                return;
            }

            InitializeProducts();
            InitializeOrders();
        }

        private static void InitializeProducts()
        {
            _context.Products.AddRange(
                new List<Product> {
                        new Product("Greetings Card", 5),
                        new Product("Binder clip", 1),
                        new Product("Cartridge paper", 3),
                        new Product("Clipboard", 7),
                        new Product("Correction fluid", 10),
                        new Product("Drawing pin", 25),
                        new Product("Envelope", 2),
                        new Product("Erasert", 6),
                        new Product("Highlighter", 18),
                        new Product("Label", 15)
                    }
            );
            _context.SaveChanges();
        }

        private static void InitializeOrders()
        {
            Order order;

            //Orders.AddRange(
            //    new List<Order> {
            // Max Lead Time = 1 - 0 extra days
            order = new Order(new DateTime(2018, 2, 12));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) }
            };
            // SaveChanges();

            // Max Lead Time = 2 - 0 extra days
            order = new Order(new DateTime(2018, 2, 12));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 2) }
            };
            // SaveChanges();

            // Max Lead Time = 3 - 0 extra days
            order = new Order(new DateTime(2018, 2, 12));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 2) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 3) }
            };
            // SaveChanges();

            // Max Lead Time = 3 - 2 extra says - order on Friday
            order = new Order(new DateTime(2018, 2, 16));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) }
            };
            // SaveChanges();

            // Max Lead Time = 3 - 2 extra days - order on Saturday
            order = new Order(new DateTime(2018, 2, 17));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 2) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 3) }
            };
            // SaveChanges();

            // Max Lead Time = 3 - 1 extra days - order on Sunday
            order = new Order(new DateTime(2018, 2, 18));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 2) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 3) }
            };
            // SaveChanges();

            // Max Lead Time = 13 - 4 extra days - order lasts 3 weeks
            order = new Order(new DateTime(2018, 3, 23));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 1) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 3) },
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 8) }
            };
            // SaveChanges();

            // Max Lead Time = 13 - 2 extra says - order lasts 2 weeks
            order = new Order(new DateTime(2018, 2, 16));
            _context.Orders.Add(order);
            order.OrderProducts = new List<OrderProduct>
            {
                new OrderProduct { Order = order, Product = _context.Products.FirstOrDefault(p => p.Id == 10) }
            };
            // SaveChanges();
            //}
            //    );
            _context.SaveChanges();
        }
    }
}
