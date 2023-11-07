using Demo3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo3.Controllers
{
    public class RegisterUserController : Controller
    {
        // GET: RegisterUser
        ShopCartEntities db = new ShopCartEntities();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu chưa
                if (IsEmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(model);
                }
                
                // Tạo một đối tượng User từ thông tin trong model
                User user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password // Lưu ý: Trong thực tế, bạn nên mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu.
                };

                // Lưu thông tin người dùng vào cơ sở dữ liệu
                db.Users.Add(user);
                db.SaveChanges();

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login", "LoginUser");
            }

            return View(model);
        }
        private bool IsEmailExists(string email)
        {
            return db.Users.Any(u => u.Email == email);
        }
    }
}