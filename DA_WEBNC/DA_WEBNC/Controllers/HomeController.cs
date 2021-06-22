using DA_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class HomeController : Controller
    {
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public async Task<ActionResult> Index()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                //var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();

                var listBT = await _database.BaiThis.ToListAsync();
                List<BaiThi> list = new List<BaiThi>();
                int count = 1;
                foreach (var item in listBT)
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
        public async Task<ActionResult> ExamList()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                //var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();

                var listBT = await _database.BaiThis.ToListAsync();
                return View(listBT);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public async Task<ActionResult> ExamTest(string IDBaiThi, string IDCauHoi, string IDBaiThiHS)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null || IDBaiThi == null)
                    return RedirectToAction("Login", "Login");

                var model = new ExamTestViewModel
                {
                    BaiThi = await _database.BaiThis.Where(x => x.IDBaiThi == IDBaiThi).FirstOrDefaultAsync(),
                    IDStudent = await _database.HocSinhs.Where(x => x.Email == email).Select(x => x.IDStudent).FirstOrDefaultAsync(),
                    IDBaiThiHS = IDBaiThiHS ?? "",
                };
                model.BaiThi.SoPhutLamBai = model.BaiThi.ThoiGianLamBai.ToString().Substring(3, 5);

                if(model.IDBaiThiHS != "")
                {
                    ViewBag.Testing = "Testing";
                }

                string idCauHoi = model.BaiThi.CTBTs.ToList()[0].IDCauHoi;
                model.CauHoi = await _database.CauHois.Where(x => x.IDCauHoi == idCauHoi).FirstOrDefaultAsync();

                var listCauHoi = new List<CauHoi>();
                foreach (var item in model.BaiThi.CTBTs)
                {
                    CauHoi c = await _database.CauHois.FindAsync(item.IDCauHoi);
                    listCauHoi.Add(c);
                }

                if (IDCauHoi != null)
                {
                    for (int count = 0; count < listCauHoi.Count; count++)
                    {
                        if (IDCauHoi == null) break;
                        if (listCauHoi[count].IDCauHoi == IDCauHoi)
                        {
                            if ((++count) == listCauHoi.Count)
                            {
                                model.CauHoi = null;
                                return View(model);
                            }
                            count -= 1;
                            model.CauHoi = listCauHoi[++count];
                            break;
                        }
                    }
                }

                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExamTest(CTBTH CTBTHS, BaiThiH BaiThiHS, string IDBaiThiHS)
        {
            if (IDBaiThiHS == "")
            {
                BaiThiHS.IDBaiThiHS = CTBTHS.IDBaiThiHS = GetIDBaiThiHS();
                _database.BaiThiHS.Add(BaiThiHS);
            }
            else
            {
                //BaiThiHS.IDBaiThiHS = CTBTHS.IDBaiThiHS = IDBaiThiHS;
                _database.BaiThiHS.Attach(BaiThiHS);
                _database.Entry(BaiThiHS).State = EntityState.Modified;
            }

            _database.CTBTHS.Add(CTBTHS);
            await _database.SaveChangesAsync();

            //CauHoi cauHoi = await _database.CauHois.Where(x => x.IDCauHoi == CTBTHS.IDCauHoi).FirstOrDefaultAsync();

            return RedirectToAction("ExamTest", new { BaiThiHS.IDBaiThi, CTBTHS.IDCauHoi, BaiThiHS.IDBaiThiHS });
        }

        [HttpPost]
        public async Task<ActionResult> Result(string IDBaiThiHS)
        {
            var BaiThiHS = await _database.BaiThiHS.FindAsync(IDBaiThiHS);
            List<CTBTH> ListCTBTHS = await _database.CTBTHS.Where(x => x.IDBaiThiHS == IDBaiThiHS).ToListAsync();
            int soCauDung = 0;
            foreach (var CTBTHS in ListCTBTHS)
            {
                if (CTBTHS.CauTraLoi == CTBTHS.DapAn)
                    soCauDung++;
            }

            BaiThiHS.TongSoDiem = soCauDung + "/" + ListCTBTHS.Count;
            _database.BaiThiHS.Attach(BaiThiHS);
            _database.Entry(BaiThiHS).State = EntityState.Modified;
            await _database.SaveChangesAsync();

            return RedirectToAction("ExamResult", "ProfileUser", new { IDBaiThiHS });
        }

        public string GetIDBaiThiHS()
        {
            var list = _database.BaiThiHS.ToArray();
            int[] listID = new int[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int.TryParse(list[i].IDBaiThiHS.Substring(4), out listID[i]);
            }
            int lastID = 0;
            for (int i = 0; i < listID.Length; i++)
            {
                if (listID[i] > lastID)
                {
                    lastID = listID[i];
                }
            }
            string ID = "BTHS" + ++lastID;
            return ID;
        }
    }
}