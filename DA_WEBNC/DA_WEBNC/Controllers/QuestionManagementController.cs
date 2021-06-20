using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DA_WEBNC.Models;

namespace DA_WEBNC.Controllers
{
    public class QuestionManagementController : Controller
    {
        private TracNghiemOnlineEntities db = new TracNghiemOnlineEntities();

        // GET: QuestionManagement
        public ActionResult Index()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var cauHois = db.CauHois.Include(c => c.DapAn);
                return View(cauHois.ToList());
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: QuestionManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // GET: QuestionManagement/Create
        public ActionResult Create()
        {
            var model = new CauHoiViewModel
            {
                cauHoi = new CauHoi { IDCauHoi = GetIDCH() },
                listCauHoi = db.CauHois.ToArray()
            };
            CauHoi cauHoi = new CauHoi();
            ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "IDCauHoi,CauHoi1,A,B,C,D,IDDapAn")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.CauHois.Add(cauHoi);
                db.SaveChanges();
                return RedirectToAction("CauHoi", new { ID = cauHoi.IDCauHoi });
            }
            ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        // GET: QuestionManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        // POST: QuestionManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCauHoi,CauHoi1,A,B,C,D,IDDapAn")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        public string GetIDCH()
        {
            //var list = db.CauHois.ToArray();

            //int.TryParse(list[list.Length - 1].IDCauHoi.Substring(2), out int lastID);

            //string ID = "CH" + ++lastID;

            //return ID;

            var list = db.CauHois.ToArray();
            int[] listID = new int[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int.TryParse(list[i].IDCauHoi.Substring(2), out listID[i]);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
