using Microsoft.AspNetCore.Hosting;
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
    public class PopularController : Controller
    {
        private readonly AppDbContext _db;

        public PopularController(AppDbContext db)
        {
            _db = db;
           
        }
        public async Task<IActionResult> Index()
        {
            List<Popular> populars = await _db.Populars.Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();

            return View(populars);
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
    }
}
