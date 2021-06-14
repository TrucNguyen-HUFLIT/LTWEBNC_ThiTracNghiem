using DA_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class HomeController : Controller
    {
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public ActionResult Index()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                //var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();

                var listBT = _database.BaiThis.ToList();
                List<BaiThi> list = new List<BaiThi>();
                int count = 1;
                foreach(var item in listBT)
                {
                    if (count == 5) break;
                    list.Add(item);
                    count++;
                }
                   return View(list);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ExamList()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                //var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();

                var listBT = _database.BaiThis.ToList();
                return View(listBT);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ExamTest(string id)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null || id == null)
                    return RedirectToAction("Login", "Login");

                var model = _database.BaiThis.Where(x => x.IDBaiThi == id).FirstOrDefault();
                model.SoPhutLamBai = model.ThoiGianLamBai.ToString().Substring(3, 5);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

    }
}