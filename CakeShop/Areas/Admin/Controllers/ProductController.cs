using CakeShop.DataAccess.Data;
using CakeShop.DataAccess.Repository.IRepository;
using CakeShop.Models;
using CakeShop.Models.ViewModels;
using CakeShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")] 
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfwork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfwork.Product.GetAll(includeProperties:"Category").ToList();
           
            return View(objProductList);
        }

        //create new Product
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfwork.Category.GetAll().Select(u => new SelectListItem
            {
                //what fields needs to be porpulated 
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //ViewBag for sending data to our View 
            //ViewBag.CategoryList = CategoryList;
           // ViewData["CategoryList"]= CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else { 
             //Update
                 productVM.Product =_unitOfwork.Product.Get(u=>u.Id==id);
                 return View(productVM);
            }
           
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file )
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old Image
                        var oldImage = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }
                    //save image 
                    using (var fileStream = new FileStream (Path.Combine(productPath, fileName),FileMode.Create)) { 
                     
                        file.CopyTo(fileStream);
                    
                    }
                    //save to Imge Url
                    productVM.Product.ImageUrl= @"\images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfwork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfwork.Product.Update(productVM.Product);
                }
               
                _unitOfwork.Save();
                TempData["Success"] = "Product Created  Successfuly";
                return RedirectToAction("Index");
            }
            else {

                productVM.CategoryList = _unitOfwork.Category.GetAll().Select(u => new SelectListItem
                {
                    //what fields needs to be porpulated 
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);

            }
            

        }
 

       

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id) {
            List<Product> objProductList = _unitOfwork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var ProductToBeDeleted = _unitOfwork.Product.Get(u=>u.Id == id);
            if (ProductToBeDeleted == null) {
                return Json(new { success = false, message = "Error While Deleting Product" });
            }
            //delete old Image
            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, ProductToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }
            _unitOfwork.Product.Remove(ProductToBeDeleted);
            _unitOfwork.Save();

            return Json(new { success = true, message = "Product Deleted " });
        }
        #endregion

    }
}
