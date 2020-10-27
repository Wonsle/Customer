using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶資料管理Controller : BaseController
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶資料管理
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var data = db.客戶資料.ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult List(int? id)
        {
            var data = db.客戶資料.Where(q => q.Id == id.Value).ToList();
            return View(data);
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

                db.客戶資料.Add(data);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(data);
        }

        public ActionResult Edit(int? id)
        {

            return View(db.客戶資料.Find(id.Value));
        }

        [HttpPost]
        public ActionResult Edit(客戶資料 data)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(data.Id);
                item.客戶名稱 = data.客戶名稱;
                item.客戶聯絡人 = data.客戶聯絡人;
                item.統一編號 = data.統一編號;
                item.電話 = data.電話;
                item.傳真 = data.傳真;
                item.地址 = data.地址;

                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(data);
        }


        public ActionResult Details(int? id)
        {

            return View(db.客戶資料.Find(id.Value));
        }

        public ActionResult Delete(int? id)
        {

            return View(db.客戶資料.Find(id.Value));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var tmp = db.客戶資料.Find(id);
                db.客戶資料.Remove(tmp);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(db.客戶資料.Find(id));
        }
    }
}