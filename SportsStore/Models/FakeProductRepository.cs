using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        //Create fake data to return prior to db creation for testing
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Football", Price = 25},
            new Product {Name = "Surfboard", Price = 179},
            new Product {Name = "Running Shoes", Price = 95 }
         }.AsQueryable<Product>();
    }

}
