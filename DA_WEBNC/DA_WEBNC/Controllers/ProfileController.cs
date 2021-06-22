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
    public class ProfileController : Controller
    {
        // GET: Profile
        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();

        public async Task<ActionResult> Index()
        {

            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            string email = Session["email"].ToString();

            var model = await _database.NhanViens.Where(x => x.Email == email).FirstOrDefaultAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Index(NhanVien nhanVien)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var model = await _database.NhanViens.Where(x => x.IDNhanVien == nhanVien.IDNhanVien).FirstOrDefaultAsync();
            nhanVien.Avatar = model.Avatar;

            if (ModelState.IsValid)
            {
                if (model.Password == HashPassword(nhanVien.Password))
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
                    _database.Entry(model).State = EntityState.Modified;
                    await _database.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(nhanVien);
            }
            return View(nhanVien);
        }

        public ActionResult ChangePassword()
        {
            try
            {
                string email = Session["email"].ToString();
                if (email != null)
                {
                    var model = _database.NhanViens.Where(x => x.Email == email).FirstOrDefault();
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
        public async Task<ActionResult> ChangePassword(ChangePassword changepass)
        {
            var model = new ViewModelNV();
            model.nhanVien = await _database.NhanViens.Where(x => x.Email == changepass.Email).FirstOrDefaultAsync();
            if (model.nhanVien != null)
            {
                if (model.nhanVien.Password == changepass.Password)
                {
                    if (changepass.NewPassword == changepass.ConfirmPass)
                    {
                        model.nhanVien.Password = changepass.NewPassword;

                        _database.NhanViens.Attach(model.nhanVien);
                        _database.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        await _database.SaveChangesAsync();
                        TempData["result"] = "Đổi mật khẩu thành công !!!";
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.ErrorPass = "Sai mật khẩu";
                    return View(model);
                }
            }
            return View();
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