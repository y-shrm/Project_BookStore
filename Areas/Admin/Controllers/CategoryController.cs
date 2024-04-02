using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using Project_BookStore.Utility;

namespace Project_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Roll_Admin)]
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();
            return View(categories);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {


            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();

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

            Category? FindCategory = _categoryRepo.Get(u => u.Id == id);

            if (FindCategory == null)
            {
                return NotFound();
            }
            return View(FindCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {


            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();

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

            Category? FindCategory = _categoryRepo.Get(u => u.Id == id);

            if (FindCategory == null)
            {
                return NotFound();
            }
            return View(FindCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();

            return RedirectToAction("Index");


        }
    }
}
