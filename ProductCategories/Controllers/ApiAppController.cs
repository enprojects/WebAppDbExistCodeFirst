using ProductCategories.Application.AppServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductCategories.Controllers
{
    public class ApiAppController : ApiController
    {
        private readonly AppService appSrv;
        private readonly int pageSize = 5;
        public ApiAppController()
        {
            appSrv = new AppService();
        }
        [HttpPost]
        public HttpResponseMessage AddProduct(Product product)
        {

            var prod = appSrv.GetAllProducts(x => x.ProductID == product.ProductID)
                .SingleOrDefault();

            if (prod != null)
            {
                prod.ProductName = product.ProductName;
                prod.CategoryID = product.CategoryID;
            }

            var result = appSrv.AddProduct(prod ?? product);
            if (result > 0)
                return Request.CreateResponse(HttpStatusCode.OK);

            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }
        [HttpGet]
        public HttpResponseMessage GetAppData(int  pageNumber)
        { 
            var products = appSrv.GetAllProducts().ToList()
                .Skip( (pageNumber -1)  * pageSize)
                .Take(pageSize);


            return Request.CreateResponse(HttpStatusCode.OK,


                new
                {
                    Products = products,
                    Categories = appSrv.GetAllCategories()
                    .OrderByDescending(x => x.CategoryID)
                });
        }
    }
}
