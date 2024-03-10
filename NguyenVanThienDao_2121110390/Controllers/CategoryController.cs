using NguyenVanThienDao_2121110390.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanThienDao_2121110390.Controllers
{
    public class CategoryController : Controller
    {
        AspEntities obj = new AspEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCategory(int id)
        {
            var prdCat = obj.Products.Where(n => n.category_id == id).ToList();
            return View(prdCat);
        }
    }
}