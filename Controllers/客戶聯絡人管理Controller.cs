using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶聯絡人管理Controller : BaseController
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶聯絡人管理
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {

            return View(db.客戶聯絡人);
        }

        [HttpPost]
        public ActionResult List(string jobTitle)
        {                        
            return View(db.客戶聯絡人.Where(q => q.職稱 == jobTitle).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(data);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(db.客戶聯絡人.Find(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(客戶聯絡人 data)
        {
            if (ModelState.IsValid)
            {
                var tmp = db.客戶聯絡人.Find(data.Id);
                tmp.姓名 = data.姓名;
                tmp.客戶資料 = data.客戶資料;
                tmp.手機 = data.手機;
                tmp.職稱 = data.職稱;
                tmp.電話 = data.電話;
                tmp.Email = data.Email;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Details(int? id)
        {

            return View(db.客戶聯絡人.Find(id.Value));
        }

        public ActionResult Delete(int? id)
        {

            return View(db.客戶聯絡人.Find(id.Value));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var tmp = db.客戶聯絡人.Find(id);
                db.客戶聯絡人.Remove(tmp);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(db.客戶聯絡人.Find(id));
        }
    }
}