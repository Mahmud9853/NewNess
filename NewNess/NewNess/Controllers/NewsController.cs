using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;

        public NewsController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            List<Popular> populars = _db.Populars.Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToList();
            List<News> news = await _db.News.Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            List<Category> categories = await _db.Categories.Include(x => x.News).Where(x => x.IsDeactive).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Populars = populars,
                News = news,
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
            News news = await _db.News.Include(x=>x.NewsImages).FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            news.Count++;
            await _db.SaveChangesAsync();
            return View(news);
        }

    }
}
