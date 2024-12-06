﻿
using CakeShop.DataAccess.Data;
using CakeShop.DataAccess.Repository.IRepository;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
                _categoryRepo = db;
        }
        public IActionResult Index()
        {
           List<Category>  objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null) {
                return NotFound();
            }
            _categoryRepo.Remove(obj); 
            _categoryRepo.Save();
            TempData["Success"] = "Category Was Deleted  Successfuly";
            return RedirectToAction("Index");

        }

    }
}
