using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewNess.DAL;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<News> news = await _db.News.Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.IsDeactive).OrderByDescending(x=>x.Id).ToListAsync();
            List<Popular> populars = await _db.Populars.Where(x => x.IsDeactive).OrderByDescending(x=>x.Id).Take(5).ToListAsync();
            List<Slider> sliders = await _db.Sliders.Where(x => x.IsDeactive).OrderByDescending(x => x.Id).Take(5).ToListAsync();
            List<Category> categories = await _db.Categories.Include(x => x.News).Where(x => x.IsDeactive).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Populars = populars,
                Sliders = sliders,
                Categories = categories
            };

            return View(homeVM);
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
        public async Task<IActionResult> NewsDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            News news = await _db.News.Include(x => x.NewsImages).FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            return View(news);
        }
        public async Task<IActionResult> PopularDetail(int? id)
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
            return View(popular);
        }
        public async Task<IActionResult> GlobalSearch(string key)
        {

            HomeVM homeVM = new HomeVM
            {
                News = await _db.News.OrderByDescending(x => x.Id).Include(x => x.Category).Include(x=>x.NewsImages).Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
                Populars = await _db.Populars.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
                Sliders = await _db.Sliders.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync()

            };

            return PartialView("_PartialGlobalSearch", homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
