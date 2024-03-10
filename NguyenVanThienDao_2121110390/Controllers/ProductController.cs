using NguyenVanThienDao_2121110390.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenVanThienDao_2121110390.Controllers
{
    public class ProductController : Controller
    {
        AspEntities obj = new AspEntities();

        // GET: Product
        public ActionResult Index()
        {
            var lstProduct = obj.Products.ToList();
            return View(lstProduct);
        }

        //DETAILS
        public ActionResult Details(int id)
        {
            var detailProduct = obj.Products.Where(n => n.id == id).FirstOrDefault();
            return View(detailProduct);
        }

        //DELETE

        public ActionResult Delete(int id)
        {
            var deleteProduct = obj.Products.FirstOrDefault(n => n.id == id);
            if (deleteProduct != null)
            {
                obj.Products.Remove(deleteProduct);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: CREATE
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product objProduct, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra xem file có dữ liệu không
                    if (file != null && file.ContentLength > 0)
                    {
                        try
                        {
                            // Tạo đường dẫn để lưu trữ ảnh
                            string path = Server.MapPath("~/Images/");
                            // Lưu trữ tên file mới
                            string fileName = Path.GetFileName(file.FileName);
                            // Đường dẫn đầy đủ để lưu trữ ảnh
                            string fullPath = Path.Combine(path, fileName);
                            // Lưu ảnh vào thư mục
                            file.SaveAs(fullPath);
                            // Gán tên ảnh vào trường sHinh của đối tượng quyen
                            objProduct.img = fileName;
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Lỗi khi tải lên ảnh: " + ex.Message);
                            return View(objProduct);
                        }
                    }

                    // Thêm đối tượng quyen vào cơ sở dữ liệu
                    obj.Products.Add(objProduct);
                    obj.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi tạo name: " + ex.Message);
            }

            return View(objProduct);
        }

        //EDIT
        public ActionResult Edit(int id)
        {
            var objProduct = obj.Products.FirstOrDefault(n => n.id == id);
            if (objProduct != null)
            {
                return View(objProduct);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm đối tượng quyen cần chỉnh sửa
                    var existingProduct = obj.Products.FirstOrDefault(n => n.id == model.id);
                    if (existingProduct != null)
                    {
                        // Kiểm tra xem có tệp ảnh mới được tải lên không
                        if (file != null && file.ContentLength > 0)
                        {
                            try
                            {
                                // Tạo đường dẫn để lưu trữ ảnh
                                string path = Server.MapPath("~/Images/");
                                // Lưu trữ tên file mới
                                string fileName = Path.GetFileName(file.FileName);
                                // Đường dẫn đầy đủ để lưu trữ ảnh
                                string fullPath = Path.Combine(path, fileName);
                                // Lưu ảnh vào thư mục
                                file.SaveAs(fullPath);
                                // Gán tên ảnh mới vào trường sHinh của đối tượng quyen
                                existingProduct.img = fileName;
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", "Lỗi khi tải lên ảnh: " + ex.Message);
                                return View(model);
                            }
                        }

                        // Cập nhật thông tin của đối tượng quyen
                        existingProduct.name = model.name;
                        existingProduct.describtion = model.describtion;
                        existingProduct.price = model.price;
                        existingProduct.status = model.status;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        obj.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi cập nhật Produt: " + ex.Message);
                }
            }

            return View(model);
        }
    }
}