using DA_WEBNC.Models;
using System.Linq;
using System.Web.Mvc;

namespace DA_WEBNC.Controllers
{
    public class StudentsManagementController : Controller
    {
        // GET: StudentsManagement
        readonly TracNghiemOnlineEntities db = new TracNghiemOnlineEntities();
        public ActionResult Index()
        {
            //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
            try
            {
                //email get value từ Session["email"]
                string email = Session["email"].ToString();
                if (email == null)
                    return RedirectToAction("Login", "Login");
                var listAccount = db.HocSinhs.ToList();
                return View(listAccount);
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }
        //public ActionResult Details()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Details(string id)
        //{
        //    //Try catch để kiểm tra Session["email"] khi chưa khởi tạo
        //    try
        //    {
        //        //email get value từ Session["email"]
        //        string email = Session["email"].ToString();
        //        if (email == null)
        //            return RedirectToAction("Login", "Login");

        //        var model = db.HocSinhs.Where(x => x.IDStudent == id).FirstOrDefault();
        //        if (model == null || id == null)
        //            return RedirectToAction("Index");
        //        return View(model);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //}
    }
}