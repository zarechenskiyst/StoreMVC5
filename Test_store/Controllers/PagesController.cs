using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_store.Models.Data;
using Test_store.Models.Data.ViewModels.Pages;

namespace Test_store.Controllers
{
    public class PagesController : Controller
    {
        // GET: Index/{page}
        public ActionResult Index(string page="")
        {
            if (page == "")
                page = "home";

            PageVM model;
            PagesDTO dto;

            using (Db db = new Db())
            {
                if (!db.Pages.Any(x => x.Slug.Equals(page)))
                    return RedirectToAction("Index", new { page = "" });
            }

            using (Db db = new Db())
            {
                dto = db.Pages.Where(x => x.Slug == page).FirstOrDefault();
            }

            ViewBag.PageTitle = dto.Title;

            model = new PageVM(dto);

            return View(model);
        }

        public ActionResult PageMenuPartial()
        {
            List<PageVM> pageVMList;

            using (Db db = new Db())
            {
                pageVMList = db.Pages.ToArray().OrderBy(x => x.Sorting).Where(x => x.Slug != "home")
                    .Select(x => new PageVM(x)).ToList();
            }
            return PartialView("_PageMenuPartial", pageVMList);
        }
    }
}