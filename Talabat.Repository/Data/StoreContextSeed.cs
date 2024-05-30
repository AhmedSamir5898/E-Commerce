using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed 
    {
        public static async Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.productBrands.Count() == 0)
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/dataSeeding/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (Brands?.Count()>0)
                {

                    foreach (var Brand in Brands)
                    {
                        await _dbcontext.productBrands.AddAsync(Brand);
                    }
                    await _dbcontext.SaveChangesAsync();

                }
            }
            if (_dbcontext.productCategory.Count() == 0)
            {
                var CatecoriesData = File.ReadAllText("../Talabat.Repository/Data/dataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CatecoriesData);

                if (categories?.Count() > 0)
                {

                    foreach (var category in categories)
                    {
                        await _dbcontext.productCategory.AddAsync(category);
                    }
                    await _dbcontext.SaveChangesAsync();

                }
            }
            if (_dbcontext.products.Count() == 0)
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/dataSeeding/products.json");
                var PRoducts = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (PRoducts?.Count() > 0)
                {

                    foreach (var product in PRoducts)
                    {
                        await _dbcontext.products.AddAsync(product);
                    }
                    await _dbcontext.SaveChangesAsync();

                }
            }

        }
    }
}
