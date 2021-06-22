using DA_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class ExamManagementController : Controller
    {
        // GET: ExamManagement
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public async Task<ActionResult> Index()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var dsBaiThi = await _database.BaiThis.ToListAsync();
            return View(dsBaiThi);
        }

        public async Task<ActionResult> Detail(string id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BaiThi baiThi = await _database.BaiThis.FindAsync(id);

            return View(baiThi);
        }

        public async Task<ActionResult> CreateBT()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            BaiThi baiThi = new BaiThi
            {
                IDBaiThi = await GetIDBT(),
            };
            return View(baiThi);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBT(BaiThi baiThi)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                _database.BaiThis.Add(baiThi);
                await _database.SaveChangesAsync();
            }
            return View("Index");
        }

        public async Task<string> GetIDBT()
        {
            var list = await _database.BaiThis.ToArrayAsync();
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
            string ID = "BT" + ++lastID;
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