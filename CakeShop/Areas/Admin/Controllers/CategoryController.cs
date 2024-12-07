using CakeShop.DataAccess.Data;
using CakeShop.DataAccess.Repository.IRepository;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfwork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        //create new Caategory
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Add(obj);
                _unitOfwork.Save();
                TempData["Success"] = "Category Created  Successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfwork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Update(obj);
                _unitOfwork.Save();
                TempData["Success"] = "Category Was Edited  Successfuly";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfwork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _unitOfwork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfwork.Category.Remove(obj);
            _unitOfwork.Save();
            TempData["Success"] = "Category Was Deleted  Successfuly";
            return RedirectToAction("Index");

        }

    }
}
