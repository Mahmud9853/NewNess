using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Areas.Admin.ViewComponents
{
    public class DetailNewsViewComponent : ViewComponent
    { 
        private readonly AppDbContext _db;

        public DetailNewsViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<News> news = await _db.News.Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.IsDeactive).Take(5).ToListAsync();
            List<Slider> sliders = await _db.Sliders.Where(x => x.IsDeactive).Take(4).ToListAsync();
            List<Category> categories = await _db.Categories.Include(x => x.News).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                News=news,
                Sliders=sliders,
                Categories = categories
            };

            return View(homeVM);
        }


    }
}
