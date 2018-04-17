using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
namespace SportsStore.Controllers
{
    public class ProductController:Controller
    {
        private IProductRepository repository;
        public ProductController (IProductRepository repo)
        {
            repository = repo;
        }
        // Because a view name is not specified, the default view for this action method is used
        public ViewResult List() => View(repository.Products);
    }
}
