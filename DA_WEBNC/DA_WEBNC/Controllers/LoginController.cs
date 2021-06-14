using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA_WEBNC.Models;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DA_WEBNC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //private readonly TracNghiemOnlineEntities _database;
        //public LoginController(TracNghiemOnlineEntities database)
        //{
        //    _database = database;
        //}

        readonly TracNghiemOnlineEntities _database = new TracNghiemOnlineEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //Mã hóa mật khẩu bằng MD5
                string password = HashPassword(loginModel.Password);

                var modelHS = _database.HocSinhs.Where(x => x.Email == loginModel.Email && x.Password == password).FirstOrDefault();
                var modelNV = _database.NhanViens.Where(x => x.Email == loginModel.Email && x.Password == password).FirstOrDefault();
                if (modelNV != null)
                {
                    //Khởi tạo (Set) Session["email"]
                    Session["email"] = loginModel.Email;
                    return RedirectToAction("Index", "Profile");
                }
                else if (modelHS != null)
                {
                    Session["email"] = loginModel.Email;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.error = "Kiểm tra lại thông tin đăng nhập";
            return View(loginModel);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var model = new HocSinh
                {
                    IDStudent = GetIDHocSinh(),
                    Email = registerModel.Email,
                    Password = HashPassword(registerModel.Password),
                    IDRole = 3
                };
                _database.HocSinhs.Add(model);
                await _database.SaveChangesAsync();
                return View("Login");

            }
            return View(registerModel);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            //Delete Session
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        public string GetIDHocSinh()
        {
            var list = _database.HocSinhs.ToArray();
            string ID = "HS" + list.Length;
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