using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewNess.DAL;
using NewNess.Helper;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewNess.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public ProfileController(IWebHostEnvironment env, AppDbContext db, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _env = env;
            _db = db;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            else
            {
                Appuser appuser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appuser == null)
                {
                    return BadRequest();
                }
                ProfileVM profileVM = new ProfileVM
                {
                    Id = appuser.Id,
                    Email = appuser.Email,
                    FullName = appuser.FullName,
                    UserName = appuser.UserName,
                    Image = appuser.Image,
                    //PhoneNumber=appuser.PhoneNumber
                };
                return View(profileVM);
            }
          
        }
        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Appuser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return BadRequest();
            }
            UpdateVM updateVM = new UpdateVM
            {

                Email = appUser.Email,
                FullName = appUser.FullName,
                UserName = appUser.UserName,
                Image = appUser.Image,
            };
            return View(updateVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, UpdateVM updateVM, Appuser appUser)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appuser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            UpdateVM dbUpdateVM = new UpdateVM
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email

            };
            if (!ModelState.IsValid)
            {
                return View(dbUpdateVM);
            }
            bool isExist = await _db.Users.AnyAsync(x => x.UserName == updateVM.UserName && x.Id != appUser.Id);
            if (isExist)
            {
                ModelState.AddModelError("", "Username is alrready exist");
                return View(dbUpdateVM);
            }

            if (appUser.Photo != null)
            {
                if (!appUser.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please choose the image flie");
                    return View(user);
                }

                string folder = Path.Combine(_env.WebRootPath, "img");
                user.Image = await appUser.Photo.SaveFileAsync(folder);
            }
            user.FullName = updateVM.FullName;
            user.UserName = updateVM.UserName;
            user.Email = updateVM.Email;
            bool selfuser = false;
            if (User.Identity.Name == dbUpdateVM.UserName)
            {
                selfuser = true;
            }
            await _userManager.UpdateAsync(user);

            if (selfuser)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);

            }
            return RedirectToAction("Index");
        }

    }


}
