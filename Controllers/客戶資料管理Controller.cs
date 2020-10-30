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
        public ActionResult List(string SortOrder)
        {
            //https://docs.microsoft.com/zh-tw/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application#add-column-sort-links            
            ViewBag.客戶分類DropDownList = new SelectList(repo.All(), "客戶分類", "客戶分類");
            
            var data = repo.All();
            switch (SortOrder)
            {
                case "客戶名稱ASC":
                    data = data.OrderBy(s => s.客戶名稱);
                    ViewBag.客戶名稱 = "客戶名稱ASC";
                    break;
                case "客戶名稱DESC":
                    data = data.OrderByDescending(s => s.客戶名稱);
                    ViewBag.客戶名稱 = "客戶名稱DESC";
                    break;
                case "統一編號ASC":
                    data = data.OrderBy(s => s.統一編號);
                    ViewBag.統一編號 = "統一編號ASC";
                    break;
                case "統一編號DESC":
                    data = data.OrderByDescending(s => s.統一編號);
                    ViewBag.統一編號 = "統一編號DESC";
                    break;
                case "電話ASC":
                    data = data.OrderBy(s => s.電話);
                    ViewBag.電話 = "電話ASC";
                    break;
                case "電話DESC":
                    data = data.OrderByDescending(s => s.電話);
                    ViewBag.電話 = "電話DESC";
                    break;
                case "傳真ASC":
                    data = data.OrderBy(s => s.傳真);
                    ViewBag.傳真 = "傳真ASC";
                    break;
                case "傳真DESC":
                    data = data.OrderByDescending(s => s.傳真);
                    ViewBag.傳真 = "傳真DESC";
                    break;
                case "地址ASC":
                    data = data.OrderBy(s => s.地址);
                    ViewBag.地址 = "地址ASC";
                    break;
                case "地址DESC":
                    data = data.OrderByDescending(s => s.地址);
                    ViewBag.地址 = "地址DESC";
                    break;
                case "EmailASC":
                    data = data.OrderBy(s => s.Email);
                    ViewBag.Email = "EmailASC";
                    break;
                case "EmailDESC":
                    data = data.OrderByDescending(s => s.Email);
                    ViewBag.Email = "EmailDESC";
                    break;
                case "客戶分類ASC":
                    data = data.OrderBy(s => s.客戶分類);
                    ViewBag.客戶分類 = "客戶分類ASC";
                    break;
                case "客戶分類DESC":
                    data = data.OrderByDescending(s => s.客戶分類);
                    ViewBag.客戶分類 = "客戶分類DESC";
                    break;
                default:
                    data = data.OrderBy(s => s.Id);
                    break;
            }
            
            return View(data);
        }
        [HttpPost]
        public ActionResult List(string customerName, FormCollection fc)
        {
            ViewBag.客戶分類DropDownList = new SelectList(repo.All(), "客戶分類", "客戶分類");
            return View(repo.All().Where(q => q.客戶名稱 == customerName).ToList());
        }
        [HttpPost]
        public ActionResult ListFromDropList(string 客戶分類)
        {
            ViewBag.客戶分類DropDownList = new SelectList(repo.All(), "客戶分類", "客戶分類");
            return View("List", repo.All().Where(q => q.客戶分類 == 客戶分類));
        }
        [HttpPost]
        public ActionResult GetReport()
        {
            CustomFile cf = repo.GetXLSXReport();
            return File(cf.FileContents, cf.ContentType, cf.FileName);
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