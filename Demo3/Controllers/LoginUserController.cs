using Demo3.Controllers;
using Demo3.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


public class LoginUserController : Controller
{
    ShopCartEntities db = new ShopCartEntities(); // Sử dụng context của bạn

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                // Lưu thông tin người dùng vào session hoặc cookie nếu cần
                Session["UserId"] = user.IDus;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index", "Home"); // Chuyển hướng sau khi đăng nhập thành công
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }
        }
        return View(model);
    }

    public ActionResult Logout()
    {
        FormsAuthentication.SignOut();
        // Xóa thông tin người dùng khỏi session hoặc cookie nếu cần
        Session.Remove("UserId");
        Session.Remove("UserName");
        return RedirectToAction("Index", "Home"); // Chuyển hướng sau khi đăng xuất
    }

}
