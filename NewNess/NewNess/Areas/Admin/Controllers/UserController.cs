using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    //[Authorize(Roles = "Member")]

    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Appuser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Appuser> _signInManager;
        public UserController(AppDbContext db, UserManager<Appuser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Appuser> signInManager)
        {
            _signInManager = signInManager;
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
          
            List<Appuser> users = await _userManager.Users.OrderByDescending(x => x.Id).ToListAsync();
            List<UserVM> userVMs = new List<UserVM>();
            foreach (Appuser user in users)
            {
                UserVM userVM = new UserVM
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id,
                    IsDeactive = user.IsDeactive,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
                userVMs.Add(userVM);
            }
            return View(userVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Appuser appUser = new Appuser
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName,

            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();

            }

            await _userManager.AddToRoleAsync(appUser, Helper.Helper.Roles.Member.ToString());

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeRole(string id)
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
            List<string> roles = new List<string>();
            roles.Add(Helper.Helper.Roles.Admin.ToString());
            roles.Add(Helper.Helper.Roles.Member.ToString());
            roles.Add(Helper.Helper.Roles.Manager.ToString());
            string oldRole = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
            ChangeRoleVM changeRole = new ChangeRoleVM
            {
                Username = appUser.UserName,
                Role = oldRole,
                Roles = roles


            };
            return View(changeRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, string newRole)
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
            List<string> roles = new List<string>();
            roles.Add(Helper.Helper.Roles.Admin.ToString());
            roles.Add(Helper.Helper.Roles.Member.ToString());
            roles.Add(Helper.Helper.Roles.Manager.ToString());
            string oldRole = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();

            ChangeRoleVM changeRole = new ChangeRoleVM
            {
                Username = appUser.UserName,
                Role = oldRole,
                Roles = roles


            };
            IdentityResult addIdentityResult = await _userManager.AddToRoleAsync(appUser, newRole);
            if (!addIdentityResult.Succeeded)
            {
                ModelState.AddModelError("", "Error");
                return View(changeRole);
            }
            IdentityResult removeIdentityResult = await _userManager.RemoveFromRoleAsync(appUser, oldRole);
            if (!removeIdentityResult.Succeeded)
            {
                ModelState.AddModelError("", "Error");
                return View(changeRole);
            }
            return RedirectToAction("Index");
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
            UpdateVM dbUpdateVM = new UpdateVM
            {
                Email = appUser.Email,
                FullName = appUser.FullName,
                UserName = appUser.UserName,
                //Image = appUser.Image,
            };
            return View(dbUpdateVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, UpdateVM updateVM)
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
            UpdateVM dbUpdateVM = new UpdateVM
            {
                Email = appUser.Email,
                FullName = appUser.FullName,
                UserName = appUser.UserName
                //Image = appUser.Image,
            };
            if (!ModelState.IsValid)
            {
                return View(dbUpdateVM);
            }
            bool IsExistEmail = await _db.Users.AnyAsync(x => x.Email == updateVM.Email && x.Id != id );
            if (IsExistEmail)
            {
                ModelState.AddModelError("Email", "Bu Email və ya İstifadəçi adı istifadə olunub yaxud yalnışdır!");
                return View(dbUpdateVM);
            }
            bool IsExistName = await _db.Users.AnyAsync(x => x.UserName == updateVM.UserName  &&  x.Id != id);
            if (IsExistName)
            {
                ModelState.AddModelError("UserName", "Bu Email və ya İstifadəçi adı istifadə olunub yaxud yalnışdır!");
                return View(dbUpdateVM);
            }
            appUser.FullName = updateVM.FullName;
            appUser.UserName = updateVM.UserName;
            appUser.Email = updateVM.Email;
            bool selfuser = false;
            if (User.Identity.Name == dbUpdateVM.UserName)
            {
                selfuser = true;
            }
            await _userManager.UpdateAsync(appUser);

            if (selfuser)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(appUser, true);

            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Activity(string id)
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
            if (appUser.IsDeactive)
            {
                appUser.IsDeactive = false;
            }
            else
            {
                appUser.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ResetPassword(string id)
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
        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id,ResetPasswordVM resetPassword)
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
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
           IdentityResult identityResult= await _userManager.ResetPasswordAsync(appUser,token,resetPassword.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> Logout()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return NotFound();
        //    }

        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Dashboard");
        //}
    }
}
