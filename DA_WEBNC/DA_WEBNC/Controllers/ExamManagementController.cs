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
        public async Task<ActionResult> CreateBT(BaiThi baiThi, int ThoiGianLamBai)
        {

            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                baiThi.ThoiGianLamBai = new TimeSpan(0, ThoiGianLamBai, 0);
                _database.BaiThis.Add(baiThi);
                await _database.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddCH(string IDBaiThi)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var listIDCH =  _database.CTBTs.Where(x => x.IDBaiThi == IDBaiThi).Select(x => x.IDCauHoi).ToList();
            var listCH = await _database.CauHois.ToListAsync();

            AddCHViewModel baiThi = new AddCHViewModel
            {
                BaiThi = await _database.BaiThis.FindAsync(IDBaiThi),
                ListCauHoi = await _database.CauHois.ToListAsync(),
            };

            foreach (var CauHoi in listCH)
            {
                foreach (var idCH in listIDCH)
                {
                    if(idCH == CauHoi.IDCauHoi)
                        baiThi.ListCauHoi.Remove(CauHoi);
                }
            }

            return View(baiThi);
        }

        [HttpPost]
        public async Task<ActionResult> AddCH(CTBT CTBT)
        {

            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                CTBT.CauHoi = await _database.CauHois.Where(x => x.IDCauHoi == CTBT.IDCauHoi).Select(x => x.CauHoi1).FirstOrDefaultAsync();
                _database.CTBTs.Add(CTBT);
                await _database.SaveChangesAsync();
            }
            return RedirectToAction("Detail",new { id= CTBT.IDBaiThi });
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