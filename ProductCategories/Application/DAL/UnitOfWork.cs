using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCategories.Application.DAL
{
    public class AppUnitOfWork
    {
        private GenericRepo<Product> productRepo;
        private GenericRepo<Category> categoryRepo;
        
        private NorthwindCtx ctx;
        public AppUnitOfWork()
        {
            ctx = new NorthwindCtx();
        }

        public GenericRepo<Category> CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                {
                    this.categoryRepo= new GenericRepo<Category>(ctx);
                }
                return categoryRepo;
            }
        }

        public GenericRepo<Product> ProductRepo
        {
           get {
                if (productRepo == null)
                {
                    this.productRepo = new GenericRepo<Product>(ctx);
                }

                return productRepo;
            }
        }
    }
}