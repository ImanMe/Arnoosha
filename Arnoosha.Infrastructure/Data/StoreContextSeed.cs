using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Arnoosha.Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Arnoosha.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Arnoosha.Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Arnoosha.Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Arnoosha.Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(e.Message);
            }
        }
    }
}