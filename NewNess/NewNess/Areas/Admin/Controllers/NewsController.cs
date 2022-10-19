using NewNess.DAL;
using NewNess.Helper;
using NewNess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using NewNess.ViewModels;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin , Manager")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public NewsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<News> news = await _db.News.Include(x => x.NewsImages).Include(x => x.Category).OrderByDescending(x=>x.Id).ToListAsync();
            //NewsImage newsImage = await _db.NewsImages.FirstOrDefaultAsync();
            return View(news);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _db.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, int categoryId)
        {
            ViewBag.Category = await _db.Categories.Where(x => x.IsDeactive).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.News.AnyAsync(x => x.Title == news.Title);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Error Name");
                return View();

            }
            if (news.Photos == null)
            {
                ModelState.AddModelError("Photos", "Error Photo");
                return View();
            }
            List<NewsImage> newsImage = new List<NewsImage>();
            foreach (IFormFile Photo in news.Photos)
            {
                if (!Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error Photo");
                    return View();
                }
                //if (!slider.Photo.IsOlder1Mb())
                //{
                //    ModelState.AddModelError("Photo", "MAX 5Mb");
                //    return View();
                //}
                string folder = Path.Combine(_env.WebRootPath, "img");
                NewsImage newsImages = new NewsImage
                {
                    Image = await Photo.SaveFileAsync(folder),
                };

                newsImage.Add(newsImages);
                news.CategoryId = categoryId;
                news.CreateTime = DateTime.UtcNow.AddHours(4);

            }
            news.NewsImages = newsImage;
            await _db.News.AddAsync(news);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteNewsImage(int? proImgId)
        {
            NewsImage newsImage = await _db.NewsImages.Include(x => x.News).ThenInclude(x => x.NewsImages).FirstOrDefaultAsync(x => x.Id == proImgId);
            int NewsImagesCount = newsImage.News.NewsImages.Count;
            string path = Path.Combine(_env.WebRootPath, "img", newsImage.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _db.NewsImages.Remove(newsImage);
            await _db.SaveChangesAsync();
            if (NewsImagesCount == 2)
            {
                return Content("stop");
            }
            return Content("Ok");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Category = await _db.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            News dbNews = await _db.News.Include(x => x.NewsImages).Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (dbNews == null)
            {
                return BadRequest();
            }
            return View(dbNews);
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
        //    if (news == null)
        //    {
        //        return BadRequest();

        //    }
        //    return View(news);

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
        //    News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
        //    if (news == null)
        //    {
        //        return BadRequest();

        //    }
        //    //news.IsDeactive = true;

        //    _db.Remove((int)id);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, News news, int categoryId)
        {
            ViewBag.Category = await _db.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            News dbNews = await _db.News.Include(x => x.NewsImages).FirstOrDefaultAsync(x => x.Id == id);
            if (dbNews == null)
            {
                return BadRequest();
            }
            bool IsExist = await _db.News.AnyAsync(x => x.Title == news.Title && x.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Name", "Error Name");
                return View(dbNews);
            }
            
            List<NewsImage> newsImage = new List<NewsImage>();
            if (news.Photos !=null)
            {
                foreach (IFormFile Photo in news.Photos)
                {
                    if (!Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Error Photo");
                        return View();
                    }
                    string folder = Path.Combine(_env.WebRootPath, "img");
                    NewsImage newsImages = new NewsImage
                    {
                        Image = await Photo.SaveFileAsync(folder),
                    };
                    newsImage.Add(newsImages);
                }
            }
            //foreach (NewsImage newsImages in dbNews.NewsImages)
            //{
            //    string path = Path.Combine(_env.WebRootPath,"img", newsImages.Image);
            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }
            //}
            dbNews.CategoryId = categoryId;
            dbNews.NewsImages.AddRange(newsImage);
            dbNews.Title = news.Title;
            dbNews.Description = news.Description;
            dbNews.CreateTime = news.CreateTime;
            dbNews.By = news.By;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            if (news.IsDeactive)
            {
                news.IsDeactive = false;
            }
            else
            {
                news.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            ViewBag.Category = await _db.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();

            }
            News product = await _db.News.Include(x => x.NewsImages).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
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
