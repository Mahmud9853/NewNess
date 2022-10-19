using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Helper;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static NewNess.Helper.Helper;

namespace NewNess.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Member")]
    //[Authorize(Roles = "Member")]

    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class PopularController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public PopularController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Popular> populars = await _db.Populars.OrderByDescending(x => x.Id).ToListAsync();
            return View(populars);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Popular popular)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Populars.AnyAsync(x => x.Title == popular.Title && x.Description == popular.Description);
            if (IsExist)
            {
                ModelState.AddModelError("", "Zəhmət olmasa xanaları doldurun !");
                return View();
            }
            if (popular.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin ! ");
                return View();
            }
            if (!popular.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin ! ");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            popular.Image = await popular.Photo.SaveFileAsync(folder);
            popular.CreateTime = DateTime.UtcNow.AddHours(4);

            await _db.Populars.AddAsync(popular);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Popular populars = await _db.Populars.FirstOrDefaultAsync(x => x.Id == id);
            if (populars == null)
            {
                return BadRequest();

            }
            populars.Count++;
            await _db.SaveChangesAsync();
            return View(populars);
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Popular Popular = await _db.Populars.FirstOrDefaultAsync(x => x.Id == id);
            if (Popular == null)
            {
                return BadRequest();
            }
            return View(Popular);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Popular popular, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Popular dbPopulars = await _db.Populars.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPopulars == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return View(dbPopulars);
            }
            bool IsExist = await _db.Populars.AnyAsync(x => x.Title == popular.Title && x.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xanaları doldurun !");
                return View(dbPopulars);
            }
            if (popular.Photo != null)
            {
                if (!popular.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error Photo");
                    return View(dbPopulars);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                dbPopulars.Image = await popular.Photo.SaveFileAsync(folder);
            }
            dbPopulars.Title = popular.Title;
            dbPopulars.Description = popular.Description;
            dbPopulars.CreateTime = popular.CreateTime;
            dbPopulars.By = popular.By;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Popular popular = await _db.Populars.FirstOrDefaultAsync(x => x.Id == id);
            if (popular == null)
            {
                return BadRequest();
            }
            if (popular.IsDeactive)
            {
                popular.IsDeactive = false;
            }
            else
            {
                popular.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> AdminSearch(string key)
        //{

        //    HomeVM homeVM = new HomeVM
        //    {
        //        News = await _db.News.OrderByDescending(x => x.Id).Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
        //        Populars = await _db.Populars.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
        //        Sliders = await _db.Sliders.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync()

        //    };

        //    return PartialView("_PartialAdminSearch", homeVM);
        //}
    }
}
