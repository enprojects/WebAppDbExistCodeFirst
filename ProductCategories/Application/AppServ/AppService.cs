using ProductCategories.Application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCategories.Application.AppServ
{
    public class AppService
    {
        private   AppUnitOfWork uow;
        public AppService()
        {
            uow = new AppUnitOfWork();
        }
        public IEnumerable<Product> GetAllProducts(Func<Product, bool> func = null)
        {
            var products = uow.ProductRepo.Get(func);
            return products;
        }
        public IEnumerable<Category> GetAllCategories(Func<Category, bool> func = null)
        {
            var  categories = uow.CategoryRepo.Get(func);
            return categories;
        }
         
        public int AddProduct(Product product)
        {
            return uow.ProductRepo.Add(product);
        }



    }
}