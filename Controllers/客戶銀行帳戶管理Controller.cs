using Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.Controllers
{
    public class 客戶銀行帳戶管理Controller : BaseController
    {
        客戶資料Entities db = new 客戶資料Entities();
        // GET: 客戶銀行帳戶管理
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {

            return View(db.客戶銀行資訊);
        }

        [HttpPost]
        public ActionResult List(string bankName)
        {
            return View(db.客戶銀行資訊.Where(q => q.銀行名稱 == bankName).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }
        [HttpPost]
        public ActionResult Create(客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(data);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(db.客戶銀行資訊.Find(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(客戶銀行資訊 data)
        {
            if (ModelState.IsValid)
            {
                var tmp = db.客戶銀行資訊.Find(data.Id);

                tmp.客戶資料 = data.客戶資料;
                tmp.帳戶名稱 = data.帳戶名稱;
                tmp.帳戶號碼 = data.帳戶號碼;
                tmp.銀行代碼 = data.銀行代碼;
                tmp.銀行名稱 = data.銀行名稱;
                tmp.分行代碼 = data.分行代碼;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(data);
        }

        public ActionResult Details(int? id)
        {

            return View(db.客戶銀行資訊.Find(id.Value));
        }

        public ActionResult Delete(int? id)
        {
            return View(db.客戶銀行資訊.Find(id.Value));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var tmp = db.客戶銀行資訊.Find(id);
                tmp.是否已刪除 = true;
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(db.客戶銀行資訊.Find(id));
        }
    }
}