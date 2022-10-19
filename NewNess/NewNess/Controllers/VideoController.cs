using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Controllers
{
    public class VideoController : Controller
    {
        private readonly AppDbContext _db;

        public VideoController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Video> Videos = await _db.Videos.Where(x=>x.IsDeactive).ToListAsync();
            return View(Videos);
        }
    }
}
