using DA_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class ExamManagementController : Controller
    {
        // GET: ExamManagement
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public ActionResult Index()
        {            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var dsBaiThi = _database.BaiThis.ToList();
                return View(dsBaiThi);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiThiViewModel baiThi = new BaiThiViewModel();
            baiThi.BaiThi = _database.BaiThis.Find(id);
            if (baiThi == null)
            {
                return HttpNotFound();
            }
            return View(baiThi);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create(BaiThi baiThi)
        {
            return View();
        }

        public string GetIDBT()
        {
            var list = _database.BaiThis.ToArray();
            int[] listID = new int[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int.TryParse(list[i].IDBaiThi.Substring(2), out listID[i]);
            }
            int lastID = 0;
            for (int i = 0; i < listID.Length; i++)
            {
                if (listID[i] > lastID)
                {
                    lastID = listID[i];
                }
            }
            string ID = "CH" + ++lastID;
            return ID;

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}