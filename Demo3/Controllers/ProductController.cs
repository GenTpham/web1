using Demo3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo3.Controllers
{
    public class ProductController : Controller
    {
        ShopCartEntities db = new ShopCartEntities();
        // GET: Product
        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = db.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }
        public ActionResult Index(string category)
        {
            if (category == null)
            {
                var productList = db.Products.OrderByDescending(x => x.Name);
                return View(productList);
            }
            else
            {
                var productList = db.Products.OrderByDescending(x => x.Name).Where(x => x.Category == category);
                return View(productList);

            }
        }
        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }
        public ActionResult SelectCate()
        {
            Category se_cate = new Category
            {
                ListCate = db.Categories.ToList<Category>()
            };
            return PartialView(se_cate);
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/sources/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/sources/images/"), filename));
                }
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
{
    // Tìm sản phẩm cần xóa dựa trên ID
        var productToDelete = db.Products.Find(id);

    if (productToDelete == null)
    {
        // Nếu không tìm thấy sản phẩm, bạn có thể xử lý lỗi hoặc chuyển hướng đến trang 404 Not Found.
        return HttpNotFound();
    }

    // Xóa sản phẩm khỏi cơ sở dữ liệu
    db.Products.Remove(productToDelete);
    db.SaveChanges();

    // Chuyển hướng đến trang Index sau khi xóa sản phẩm thành công
    return RedirectToAction("Index");
}

    }
}