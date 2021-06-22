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
        private readonly TracNghiemOnlineEntities db = new TracNghiemOnlineEntities();

        // GET: QuestionManagement
        public async Task<ActionResult> Index(string IDCauHoi)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var listCauHois = await db.CauHois.ToListAsync();
                if (IDCauHoi != null)
                {
                    listCauHois = new List<CauHoi>
                    {
                        await db.CauHois.FindAsync(IDCauHoi),
                    };
                    ViewBag.Added = "Added";
                }
                return View(listCauHois);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }


        // GET: QuestionManagement/Create
        public async Task<ActionResult> Create()
        {
            var model = new CauHoiViewModel
            {
                cauHoi = new CauHoi { IDCauHoi = await GetIDCH() },
                listCauHoi = await db.CauHois.ToArrayAsync()
            };
            //CauHoi cauHoi = new CauHoi();
            //ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(/*[Bind(Include = "IDCauHoi,CauHoi1,A,B,C,D,DapAn")]*/ CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.CauHois.Add(cauHoi);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { cauHoi.IDCauHoi });
            }
            //ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        // GET: QuestionManagement/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = await db.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        // POST: QuestionManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDCauHoi,CauHoi1,A,B,C,D,DapAn")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHoi).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.IDDapAn = new SelectList(db.DapAns, "IDDapAn", "DapAn1", cauHoi.IDDapAn);
            return View(cauHoi);
        }

        public async Task<string> GetIDCH()
        {
            //var list = db.CauHois.ToArray();

            //int.TryParse(list[list.Length - 1].IDCauHoi.Substring(2), out int lastID);

            //string ID = "CH" + ++lastID;

            //return ID;

            var list = await db.CauHois.ToArrayAsync();
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
