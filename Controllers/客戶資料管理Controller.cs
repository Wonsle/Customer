using Customer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶資料管理Controller : BaseController
    {

        客戶資料Repository repo;
        public 客戶資料管理Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }

        [HttpGet]
        public ActionResult List()
        {
            ViewBag.客戶分類 = new SelectList(repo.All(), "客戶分類", "客戶分類");

            return View(repo.All());
        }
        [HttpPost]
        public ActionResult List(string customerName)
        {
            ViewBag.客戶分類 = new SelectList(repo.All(), "客戶分類", "客戶分類");
            return View(repo.All().Where(q => q.客戶名稱 == customerName).ToList());
        }
        [HttpPost]
        public ActionResult ListFromDropList(string 客戶分類)
        {
            ViewBag.客戶分類 = new SelectList(repo.All(), "客戶分類", "客戶分類");
            return View("List",repo.All().Where(q => q.客戶分類 == 客戶分類));
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                repo.Add(data);
                repo.UnitOfWork.Commit();
                return RedirectToAction("List");
            }

            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            return View(repo.FindById(id.Value));
        }

        [HttpPost]
        public ActionResult Edit(客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                var item = repo.FindById(data.Id);
                item.InjectFrom(data);
                repo.UnitOfWork.Commit();
                return RedirectToAction("List");
            }

            return View(data);
        }


        public ActionResult Details(int? id)
        {
            return View(repo.FindById(id.Value));
        }

        public ActionResult Delete(int? id)
        {
            return View(repo.FindById(id.Value));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var tmp = repo.FindById(id);
                repo.Delete(tmp);
                repo.UnitOfWork.Commit();

                return RedirectToAction("List");
            }

            return View(repo.FindById(id));
        }
    }
}