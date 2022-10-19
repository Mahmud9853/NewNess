using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class VideoController : Controller
    {
        private readonly AppDbContext _db;

        public VideoController(AppDbContext db)
        {
            _db = db;

        }
        public async Task<IActionResult> Index()
        {
            List<Video> Videos = await _db.Videos.OrderByDescending(x => x.Id).ToListAsync();
            return View(Videos);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Video video)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Videos.AnyAsync(x => x.Title == video.Title && x.Link == video.Link);
            if (IsExist)
            {
                ModelState.AddModelError("", "Zəhmət olmasa xanaları doldurun !");
                return View();
            }
            //video.CreateTime = DateTime.UtcNow.AddHours(4);
            await _db.Videos.AddAsync(video);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Video video = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
        //    if (video == null)
        //    {
        //        return BadRequest();

        //    }
        //    return View(video);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Video video = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
        //    if (video == null)
        //    {
        //        return BadRequest();

        //    }
        //    video.IsDeactive = true;

        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Video video = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                return BadRequest();
            }
            return View(video);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Video video, int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Video dbVideo = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (dbVideo == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return View(dbVideo);
            }
            bool IsExist = await _db.Populars.AnyAsync(x => x.Title == video.Title && x.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xanaları doldurun !");
                return View(dbVideo);
            }
            dbVideo.Title = video.Title;
            dbVideo.Link = video.Link;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Video video = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                return BadRequest();
            }
            return View(video);
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Video video = await _db.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
            {
                return BadRequest();
            }
            if (video.IsDeactive)
            {
                video.IsDeactive = false;
            }
            else
            {
                video.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}