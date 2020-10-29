using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer.Models;
namespace Customer.Controllers
{
    public class 客戶統計資訊Controller : Controller
    {
        客戶資料Entities db = new 客戶資料Entities();
        
        public ActionResult Index()
        {
            return View(db.客戶統計檢視);
        }
    }
}