using AutoMapper;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using ECommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {

        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 300,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem()
                        {
                            Id=1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice = 30
                        }
                    }
                });

                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 200,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem()
                        {
                            Id=2,
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 5,
                            UnitPrice = 20
                        },
                        new Db.OrderItem()
                        {
                            Id=3,
                            OrderId = 2,
                            ProductId = 5,
                            Quantity = 2,
                            UnitPrice = 50
                        }
                    }
                });

                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Total = 300,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem()
                        {
                            Id=4,
                            OrderId = 3,
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice = 30
                        }
                    }
                });

                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 4,
                    CustomerId = 3,
                    OrderDate = DateTime.Now,
                    Total = 200,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem()
                        {
                            Id=5,
                            OrderId = 4,
                            ProductId = 2,
                            Quantity = 5,
                            UnitPrice = 30
                        },
                        new Db.OrderItem()
                        {
                            Id=6,
                            OrderId = 4,
                            ProductId = 3,
                            Quantity = 5,
                            UnitPrice = 100
                        }
                    }
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
