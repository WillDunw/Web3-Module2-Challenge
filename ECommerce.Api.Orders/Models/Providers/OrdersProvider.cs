using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Models.Providers
{
    public class ProductsProvider : IProductsProvider
    {

        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "KeyBoard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 50});
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 200 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
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

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var products = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (products != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(products);
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
