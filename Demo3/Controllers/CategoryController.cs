using Demo3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo3.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        ShopCartEntities db = new ShopCartEntities(); // Thay thế YourDbContext bằng context của bạn

        // Hành động để hiển thị danh sách danh mục
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(db.Categories.ToList());
            else
                return View(db.Categories.Where(s => s.NameCate.Contains(_name)).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View(db.Categories.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(db.Categories.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Delete(int id)
        {
            return View(db.Categories.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = db.Categories.Where(s => s.ID == id).FirstOrDefault();
                db.Categories.Remove(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This is data using in other table, Error Delete!");
            }
        }
    }
}