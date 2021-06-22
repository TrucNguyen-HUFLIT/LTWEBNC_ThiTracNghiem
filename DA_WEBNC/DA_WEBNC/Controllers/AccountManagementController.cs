using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DA_WEBNC.Models;

namespace DA_WEBNC.Controllers
{
    public class AccountManagementController : Controller
    {
        // GET: AccountManagement
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
                var listAccount = _database.NhanViens.ToList();
                return View(listAccount);
            }
            catch 
            {
                return RedirectToAction("Login", "Login");

            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(NhanVien nhanVien)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");

                if (ModelState.IsValid)
                {
                    if (nhanVien.UploadAvt != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(nhanVien.UploadAvt.FileName);
                        string extent = Path.GetExtension(nhanVien.UploadAvt.FileName);
                        filename += extent;
                        nhanVien.Avatar = "/Content/images/" + filename;
                        nhanVien.UploadAvt.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), filename));
                    }
                    else
                    {
                        nhanVien.Avatar = "/Content/Images/avatar-default-icon.png";
                    }
                    nhanVien.Password = HashPassword(nhanVien.Password);
                    nhanVien.IDNhanVien = GetIDNhanVien();
                    _database.NhanViens.Add(nhanVien);
                    await _database.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(nhanVien);
            }
            catch
            {
                return RedirectToAction("Login", "Login");

            }
        }
        public ActionResult Edit(string id)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");

                var model = _database.NhanViens.Where(x => x.IDNhanVien == id).FirstOrDefault();
                if (model == null || id == null)
                    return RedirectToAction("Index");
                return View(model);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(NhanVien nhanVien)
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var model = _database.NhanViens.Where(x => x.IDNhanVien == nhanVien.IDNhanVien).FirstOrDefault();
                nhanVien.Avatar = model.Avatar;
                if (ModelState.IsValid)
                {
                    model.Name = nhanVien.Name;
                    model.Address = nhanVien.Address;
                    model.Email = nhanVien.Email;

                    if (nhanVien.UploadAvt != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(nhanVien.UploadAvt.FileName);
                        string extent = Path.GetExtension(nhanVien.UploadAvt.FileName);
                        filename += extent;
                        model.Avatar = "/Content/images/" + filename;
                        nhanVien.UploadAvt.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), filename));
                    }

                    _database.NhanViens.Attach(model);
                    _database.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    await _database.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(nhanVien);
            }
            catch
            {
                return RedirectToAction("Login", "Login");

            }
        }
        public string GetIDNhanVien()
        {
            var list = _database.NhanViens.ToArray();
            int[] listID = new int[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                int.TryParse(list[i].IDNhanVien.Substring(2), out listID[i]);
            }
            int lastID = 0;
            for (int i = 0; i < listID.Length; i++)
            {
                if (listID[i] > lastID)
                {
                    lastID = listID[i];
                }
            }
            string ID = "NV" + ++lastID;
            return ID;
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