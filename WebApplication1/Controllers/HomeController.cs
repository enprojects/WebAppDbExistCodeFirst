using System.Web.Mvc;
using WebApplication1.Application;
using System.Linq;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        private readonly AppUnitOfWork uow;
        public HomeController()
        {
            uow = new AppUnitOfWork();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProducts()
        {
            return Json(new   {
                Products = uow.ProductRepo.Get(),
               Categories  = uow.CategoryRepo.Get()
            }, JsonRequestBehavior.AllowGet);
        }



    }
}