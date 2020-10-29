using Customer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶銀行帳戶管理Controller : BaseController
    {

        客戶銀行資訊Repository repo;
        客戶資料Repository 客戶資料repo;

        public 客戶銀行帳戶管理Controller()
        {
            repo = RepositoryHelper.Get客戶銀行資訊Repository();
            客戶資料repo = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {

            return View(repo.All());
        }

        [HttpPost]
        public ActionResult List(string bankName)
        {
            return View(repo.All().Where(q => q.銀行名稱 == bankName).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            return View();
        }
        [HttpPost]
        public ActionResult Create(客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                repo.Add(data);
                repo.UnitOfWork.Commit();

                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", id);
            return View(repo.FindById(id.Value));

        }
        [HttpPost]
        public ActionResult Edit(客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                var tmp = repo.FindById(data.Id);
                tmp.InjectFrom(data);
                repo.UnitOfWork.Commit();


                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", data.Id);
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