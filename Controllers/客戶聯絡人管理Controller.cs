using Customer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶聯絡人管理Controller : BaseController
    {
        //客戶資料Entities db = new 客戶資料Entities();
        客戶聯絡人Repository repo;
        客戶資料Repository 客戶資料repo;
        public 客戶聯絡人管理Controller()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
            客戶資料repo = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }

        [HttpGet]
        public ActionResult List(string SortOrder, FormCollection fc)
        {
            var data = repo.All();
            switch (SortOrder)
            {
                case "職稱ASC":
                    data = data.OrderBy(s => s.職稱);
                    ViewBag.職稱 = "職稱ASC";
                    break;
                case "職稱DESC":
                    data = data.OrderByDescending(s => s.職稱);
                    ViewBag.職稱 = "職稱DESC";
                    break;
                case "姓名ASC":
                    data = data.OrderBy(s => s.姓名);
                    ViewBag.姓名 = "姓名ASC";
                    break;
                case "姓名DESC":
                    data = data.OrderByDescending(s => s.姓名);
                    ViewBag.姓名 = "姓名DESC";
                    break;
                case "EmailASC":
                    data = data.OrderBy(s => s.Email);
                    ViewBag.Email = "EmailASC";
                    break;
                case "EmailDESC":
                    data = data.OrderByDescending(s => s.Email);
                    ViewBag.Email = "EmailDESC";
                    break;
                case "手機ASC":
                    data = data.OrderBy(s => s.手機);
                    ViewBag.手機 = "手機ASC";
                    break;
                case "手機DESC":
                    data = data.OrderByDescending(s => s.手機);
                    ViewBag.手機 = "手機DESC";
                    break;
                case "電話ASC":
                    data = data.OrderBy(s => s.電話);
                    ViewBag.電話 = "電話ASC";
                    break;
                case "電話DESC":
                    data = data.OrderByDescending(s => s.電話);
                    ViewBag.電話 = "電話DESC";
                    break;
                case "客戶名稱ASC":
                    data = data.OrderBy(s => s.客戶資料.客戶名稱);
                    ViewBag.客戶名稱 = "客戶名稱ASC";
                    break;
                case "客戶名稱DESC":
                    data = data.OrderByDescending(s => s.客戶資料.客戶名稱);
                    ViewBag.客戶名稱 = "客戶名稱DESC";
                    break;
                default:
                    data = data.OrderBy(s => s.Id);
                    break;
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult List(string jobTitle)
        {
            return View(repo.All().Where(q => q.職稱 == jobTitle));
        }

        [HttpPost]
        public ActionResult GetReport()
        {
            CustomFile cf = repo.GetXLSXReport();
            return File(cf.FileContents, cf.ContentType, cf.FileName);
        }
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 data)
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
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", id.Value);

            return View(repo.FindById(id.Value));

        }
        [HttpPost]
        public ActionResult Edit(客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {

                var tmp = repo.FindById(data.Id);
                tmp.InjectFrom(data);
                repo.UnitOfWork.Commit();

                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", data.客戶Id);
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