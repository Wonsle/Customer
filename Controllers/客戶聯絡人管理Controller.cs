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
    }
}