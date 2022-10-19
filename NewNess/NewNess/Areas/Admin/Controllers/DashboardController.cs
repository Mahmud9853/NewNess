using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public DashboardController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AdminSearch(string key)
        {

            HomeVM homeVM = new HomeVM
            {
                News = await _db.News.OrderByDescending(x => x.Id).Include(x => x.Category).Include(x => x.NewsImages).Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
                Populars = await _db.Populars.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync(),
                Sliders = await _db.Sliders.Where(x => x.Title.Contains(key) && x.IsDeactive).ToListAsync()

            };

            return PartialView("_PartialAdminSearch", homeVM);
        }
    }
}
