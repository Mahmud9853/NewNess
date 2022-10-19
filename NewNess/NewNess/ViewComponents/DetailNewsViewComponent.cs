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
    public class DetailSliderViewComponent : ViewComponent
    { 
        private readonly AppDbContext _db;

        public DetailSliderViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<News> news = await _db.News.Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.IsDeactive).Take(5).ToListAsync();
    
            List<Category> categories = await _db.Categories.Include(x => x.News).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                News=news,
       
                Categories = categories
            };

            return View(homeVM);
        }


    }
}
