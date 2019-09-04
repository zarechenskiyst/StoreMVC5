using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_store.Models.Data;
using Test_store.Models.Data.ViewModels.Shop;

namespace Test_store.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Pages");
        }

        public ActionResult CategoryMenuPartial()
        {
            List<CategoryVM> model;

            using (Db db = new Db())
            {
                model = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVM(x)).ToList();
            }

            return PartialView("_CategoryMenuPartial",model);
        }
        //GET: Shop/category/name
        public ActionResult Category(string name)
        {
            List<ProductVM> productVMList;

            using (Db db = new Db())
            {
                CategoryDTO categoryDTO = db.Categories.Where(x => x.Slug == name).FirstOrDefault();

                int catId = categoryDTO.Id;

                productVMList = db.Products.ToArray().Where(x => x.CategoryId == catId).Select(x => new ProductVM(x)).ToList();

                var productCat = db.Products.Where(x => x.CategoryId == catId).FirstOrDefault();

                if (productCat == null)
                {
                    var catName = db.Categories.Where(x => x.Slug == name).Select(x => x.Name).FirstOrDefault();
                    ViewBag.CategoryName = catName;
                }
                else
                {
                    ViewBag.CategoryName = productCat.CategoryName;
                }
            }
                return View(productVMList);
        }

        //GET: Shop/product-details/name
        [ActionName("product-details")]
        public ActionResult ProductDetails(string name)
        {
            ProductDTO dto;
            ProductVM model;

            using (Db db = new Db())
            {
                if (!db.Products.Any(x => x.Slug.Equals(name)))
                    return RedirectToAction("Index", "Shop");

                dto = db.Products.Where(x => x.Slug == name).FirstOrDefault();

                model = new ProductVM(dto);
                 
            }
            return View("ProductDetails", model);
        }
    }
}