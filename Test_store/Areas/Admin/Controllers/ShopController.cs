using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_store.Models.Data;
using Test_store.Models.Data.ViewModels.Shop;
using PagedList;

namespace Test_store.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop
        public ActionResult Categories()
        {
            List<CategoryVM> categoryVMList;

            using (Db db = new Db())
            {
                categoryVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVM(x)).ToList();
            }

            return View(categoryVMList);
        }

        // GET: Admin/Shop/AddNewCategoty
        [HttpGet]
        public ActionResult AddNewCategory()
        {
            return View();
        }
        // POST: Admin/Shop/AddNewCategory

        [HttpPost]
        public ActionResult AddNewCategory(CategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                //string slug;
                CategoryDTO dto = new CategoryDTO();


                if (db.Pages.Any(x => x.Title == model.Name))
                {
                    ModelState.AddModelError("", "That category name already exist");
                    return View(model);
                }
                else
                {
                    dto.Name = model.Name;
                    dto.Slug = model.Name.Replace(" ", "-").ToLower();
                    dto.Sorting = 100;
                }

                db.Categories.Add(dto);
                db.SaveChanges();


            }

            TempData["SM"] = "You have added a new category";


            return RedirectToAction("Categories");
        }

        // GET: Admin/Shop/DeleteCategory/id
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            using (Db db = new Db())
            {
                CategoryDTO dto = db.Categories.Find(id);
                db.Categories.Remove(dto);
                db.SaveChanges();
            }

            TempData["SM"] = "You have deleted a Category";

            return RedirectToAction("Categories");
        }

        // GET: Admin/Shop/AddProduct/id
        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductVM model = new ProductVM();

            using (Db db = new Db())
            {
                model.Categories = new SelectList(db.Categories.ToList(), dataValueField: "Id", dataTextField: "Name");
            }

            return View(model);
        } 

        // POST: Admin/Shop/AddProduct/id
        [HttpPost]
        public ActionResult AddProduct(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    model.Categories = new SelectList(db.Categories.ToList(), dataValueField: "Id", dataTextField: "Name");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                if (db.Products.Any(x => x.Name == model.Name))
                {
                    model.Categories = new SelectList(db.Categories.ToList(), dataValueField: "Id", dataTextField: "Name");
                    ModelState.AddModelError("", "That category name already exist");
                    return View(model);
                }
            }
            using (Db db = new Db())
            {
                ProductDTO dto = new ProductDTO();

                dto.Name = model.Name;
                dto.Price = model.Price;
                dto.Slug = model.Name.Replace(" ", "-").ToLower();
                dto.Description = model.Description;
                dto.CategoryId = model.CategoryId;

                CategoryDTO catDto = db.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
                dto.CategoryName = catDto.Name;

                db.Products.Add(dto);
                db.SaveChanges();

            }

            TempData["SM"] = "You have added a new product";


            return RedirectToAction("AddProduct");
        }

        // GET: Admin/Shop/Products
        [HttpGet]
        public ActionResult Products(int? page, int? catId)
        {
            List<ProductVM> listOfProductVM ;

            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {
                listOfProductVM = db.Products.ToArray()
                    .Where(x => catId == null || catId == 0 || x.CategoryId == catId)
                    .Select(x => new ProductVM(x)).ToList();

                ViewBag.Categories = new SelectList(db.Categories.ToList(), dataValueField: "Id", dataTextField: "Name");

                ViewBag.SelectedCat = catId.ToString();
            }

            var onePageOfProducts = listOfProductVM.ToPagedList(pageNumber, 3);

            ViewBag.onePageOfProducts = onePageOfProducts;

            return View(listOfProductVM);
        }
    }

}
