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
        {
            var dsBaiThi = _database.BaiThis.ToList();
            return View(dsBaiThi);
        }

        public ActionResult Create()
        {
            return View();
        }
        //public ActionResult Details( string id, string idCauHoi)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    //cái bước kia bị thừa
        //    //.Find(id) == .Where(x=>x.IDBaiThi == id).FirstOrDefaultAsync(); tựa nhau

        //    BaiThiViewModel baiThiViewModel = new BaiThiViewModel();
        //    baiThiViewModel.CTBTHS = _database.CTBTHS.Find(id);
        //    baiThiViewModel.ListCauHoi = _database.CauHois.ToList();
        //    if (baiThiViewModel.CTBTHS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(baiThiViewModel);
        //}
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