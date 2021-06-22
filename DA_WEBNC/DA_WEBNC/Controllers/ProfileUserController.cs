using DA_WEBNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class ProfileUserController : Controller
    {
        // GET: ProfileUser
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public ActionResult Index()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email != null)
                {
                    var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();
                    StaticAcc.Name = model.Name;
                    return View(model);
                }
                return RedirectToAction("Login", "Login");
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public async Task<ActionResult> Index(HocSinh hocSinh)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
            var model = _database.HocSinhs.Where(x => x.IDStudent == hocSinh.IDStudent).FirstOrDefault();
            hocSinh.Avatar = model.Avatar;

            if (ModelState.IsValid)
            {
                if (model.Password == HashPassword(hocSinh.Password))
                {
                    model.Name = hocSinh.Name;
                    model.Email = hocSinh.Email;

                    if (hocSinh.UploadAvt != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(hocSinh.UploadAvt.FileName);
                        string extent = Path.GetExtension(hocSinh.UploadAvt.FileName);
                        filename += extent;
                        model.Avatar = "/Content/images/" + filename;
                        hocSinh.UploadAvt.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), filename));
                    }

                    _database.HocSinhs.Attach(model);
                    _database.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    await _database.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(hocSinh);
            }
            return View(hocSinh);
        }


        public ActionResult ExamHistory()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var model = _database.HocSinhs.Where(x => x.Email == email).FirstOrDefault();

                var listBT = _database.BaiThiHS.Where(x => x.IDStudent == model.IDStudent).ToList();
                foreach (var item in listBT)
                {
                    item.BaiThi = _database.BaiThis.Where(x => x.IDBaiThi == item.IDBaiThi).FirstOrDefault();
                }
                return View(listBT);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public async Task<ActionResult> ExamResult(string IDBaiThiHS)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");

                var model = new ExamResultViewModel
                {
                    ListCTBTHS = await _database.CTBTHS.Where(x => x.IDBaiThiHS == IDBaiThiHS).ToListAsync(),
                    TongSoDiem = await _database.BaiThiHS.Where(x=>x.IDBaiThiHS == IDBaiThiHS).Select(x=>x.TongSoDiem).FirstOrDefaultAsync(),
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public string HashPassword(string password)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn thay chữ "X" in hoa trong "X2" thành "x"
            return sb.ToString();
        }
    }
}