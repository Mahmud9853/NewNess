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

namespace NewNess.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.OrderByDescending(x => x.Id).ToListAsync();
            return View(sliders);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Slider sliders = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (sliders == null)
            {
                return BadRequest();
            }
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {

            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            bool IsExist = await _db.Sliders.AnyAsync(x => x.Title == slider.Title && x.Description == slider.Description);
            if (IsExist)
            {
                ModelState.AddModelError("", "Zəhmət olmasa xanaları doldurun !");
                return BadRequest();
            }
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin ! ");
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin ! ");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            slider.Image = await slider.Photo.SaveFileAsync(folder);

            slider.CreateTime = DateTime.UtcNow.AddHours(4);
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Slider sliders = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (sliders == null)
            {
                return BadRequest();
            }
            return View(sliders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Slider slider, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSliders = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSliders == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return View(dbSliders);
            }
            bool IsExist = await _db.Sliders.AnyAsync(x => x.Title == slider.Title && x.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xanaları doldurun !");
                return View(dbSliders);
            }
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error Photo");
                    return View(dbSliders);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                dbSliders.Image = await slider.Photo.SaveFileAsync(folder);
            }
            dbSliders.Title = slider.Title;
            dbSliders.Description = slider.Description;
            dbSliders.CreateTime = slider.CreateTime;
            dbSliders.By = slider.By;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            if (slider.IsDeactive)
            {
                slider.IsDeactive = false;
            }
            else
            {
                slider.IsDeactive = true;
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
