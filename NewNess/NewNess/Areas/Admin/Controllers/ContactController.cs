using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Helper;
using NewNess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    [Authorize(Roles = "Admin , Manager")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ContactController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Contact> contact = await _db.Contacts.ToListAsync();
            return View(contact);
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
            {
                return BadRequest();
            }
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Contact contact, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact dbContact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbContact == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return View(dbContact);
            }
            bool IsExist = await _db.Contacts.AnyAsync(x => x.Number == contact.Number && x.Id != id);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xanaları doldurun !");
                return View(dbContact);
            }
            if (contact.Photos != null)
            {
                if (!contact.Photos.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error Photo");
                    return View(dbContact);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                dbContact.NewsImages = await contact.Photos.SaveFileAsync(folder);
            }
            dbContact.Number = contact.Number;
            dbContact.Email = contact.Email;
            dbContact.InstagramLink = contact.InstagramLink;
            dbContact.InstagramName = contact.InstagramName;
            dbContact.YoutubeLink = contact.YoutubeLink;
            dbContact.YoutubemName = contact.YoutubemName;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
            {
                return BadRequest();
            }
            if (contact.IsDeactive)
            {
                contact.IsDeactive = false;
            }
            else
            {
                contact.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Contact contact)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    bool IsExist = await _db.Contacts.AnyAsync(x => x.Number == contact.Number && x.Email == contact.Email);
        //    if (IsExist)
        //    {
        //        ModelState.AddModelError("", "Zəhmət olmasa xanaları doldurun !");
        //        return View();
        //    }
        //    if (contact.Photos == null)
        //    {
        //        ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin ! ");
        //        return View();
        //    }
        //    if (!contact.Photos.IsImage())
        //    {
        //        ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin ! ");
        //        return View();
        //    }
        //    string folder = Path.Combine(_env.WebRootPath, "img");
        //    contact.NewsImages = await contact.Photos.SaveFileAsync(folder);

        //    await _db.Contacts.AddAsync(contact);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
