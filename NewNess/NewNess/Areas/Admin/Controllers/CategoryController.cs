using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Member")]
    [Area("Admin")]
    [Authorize(Roles = "Admin , Manager")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Categories.AnyAsync(x => x.Name == category.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Category categories = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null)
            {

                return BadRequest();
            }
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Category category, int? id, int categoryId)
        {
            if (id == null)
            {
                return NotFound();

            }
            Category dbcategories = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategories == null)
            {

                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbcategories);
            }
            bool IsExist = await _db.Categories.AnyAsync(x => x.Name == category.Name);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Zəhmət olmasa xananı doldurun !");
                return View(dbcategories);
            }
            dbcategories.Name = category.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            if (category.IsDeactive)
            {
                category.IsDeactive = false;
            }
            else
            {
                category.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

