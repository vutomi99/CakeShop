using CakeShop.Data;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
           List<Category>  objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //create new Caategory
        public IActionResult Create() { 
         
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created  Successfuly";
                return RedirectToAction("Index");
            }
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
           
            if (id == null || id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj= _db.Categories.Find(id);
            if (obj == null) {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Was Deleted  Successfuly";
            return RedirectToAction("Index");

        }

    }
}
