using Demo3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo3.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
            ShopCartEntities db = new ShopCartEntities();

            // Hiển thị danh sách sản phẩm
            public ActionResult Products()
            {
                var products = db.Products.ToList();
                return View(products);
            }

            // Hiển thị biểu mẫu để thêm sản phẩm mới
            public ActionResult CreateProduct()
            {
                var categories = db.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "IDCate", "NameCate");
                return View();
            }

            // Xử lý tạo sản phẩm mới
            [HttpPost]
            public ActionResult CreateProduct(Product product)
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Products");
                }

                var categories = db.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "IDCate", "NameCate");
                return View(product);
            }

            // Hiển thị danh sách đơn hàng
            public ActionResult Orders()
            {
                var orders = db.Orders.ToList();
                return View(orders);
            }

            // Hiển thị chi tiết đơn hàng
            public ActionResult OrderDetails(int id)
            {
                var order = db.Orders.Find(id);
                return View(order);
            }

            // Cập nhật trạng thái đơn hàng
            [HttpPost]
            public ActionResult UpdateOrderStatus(int id, string status)
            {
                var order = db.Orders.Find(id);
                if (order != null)
                {
                    
                    db.SaveChanges();
                }
                return RedirectToAction("Orders");
            }

            // Xóa sản phẩm
            public ActionResult DeleteProduct(int id)
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Products");
            }

            // Hiển thị danh sách người dùng
            public ActionResult Users()
            {
                var users = db.Users.ToList();
                return View(users);
            }

            // Xóa người dùng
            public ActionResult DeleteUser(int id)
            {
                var user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Users");
            }
        
    }
}