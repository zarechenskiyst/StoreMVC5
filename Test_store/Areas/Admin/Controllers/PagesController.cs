using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_store.Models.Data;
using Test_store.Models.Data.ViewModels.Pages;

namespace Test_store.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Обьявляем список для представляения (pageVM)

            //Инициализируем список (DB)

            //возвращаем список в представление

            List<PageVM> pageList;

            using (Db db = new Db())
            {
                pageList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }

            return View(pageList);
        }
        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            
            return View();
        }

        // POST: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                string slug;
                PagesDTO dto = new PagesDTO();
                dto.Title = model.Title.ToUpper();

                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else slug = model.Slug.Replace(" ", "-").ToLower();

                if (db.Pages.Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist");
                    return View(model);
                }
                else if(db.Pages.Any(x=>x.Slug==model.Slug))
                {
                    ModelState.AddModelError("", "That slug already exist");
                    return View(model);
                }

                dto.Slug = slug;
                dto.Body = model.Body;
                dto.Sorting = 100;

                db.Pages.Add(dto);
                db.SaveChanges();


            }

            TempData["SM"] = "You have added a new page";

            return RedirectToAction("Index");

        }

        // GET: Admin/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PageVM model;

            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);

                if (dto == null)
                    return Content("This page does not exist");

                model = new PageVM(dto);
            }


            return View(model);
        }

        // POST: Admin/Pages/EditPage
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (Db db = new Db())
            {
                int id = model.Id;
                string slug="home";

                PagesDTO dto = db.Pages.Find(id);

                dto.Title = model.Title;
                if (model.Slug!="home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                        slug = model.Title.Replace(' ', '-').ToLower();
                    else
                        slug = model.Slug.Replace(' ', '-').ToLower();
                }

                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title))
                {
                    ModelState.AddModelError("", "That title already exist");
                    return View(model);
                }
                else if (db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist");
                    return View(model);
                }

                dto.Slug = slug;
                dto.Body = model.Body;

                db.SaveChanges();
            }

            TempData["SM"] = "You have edit this page";

            return RedirectToAction("EditPage");
        }

        // GET: Admin/Pages/PageDetails/id
        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            PageVM model;

            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);
                if (dto == null)
                    return Content("This page does not exist");

                model = new PageVM(dto);
            }

            return View(model);
        }

        // GET: Admin/Pages/DeletePage/id
        [HttpGet]
        public ActionResult DeletePage(int id)
        {
            using(Db db=new Db())
            {
                PagesDTO dto = db.Pages.Find(id);
                db.Pages.Remove(dto);
                db.SaveChanges();
            }

            TempData["SM"] = "You have deleted a page";

            return RedirectToAction("Index");
        }
    }


}