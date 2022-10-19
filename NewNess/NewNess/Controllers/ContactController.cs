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
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;

        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            Contact contacts = await _db.Contacts.FirstOrDefaultAsync();
       
            return View(contacts);
        }
    }
}
