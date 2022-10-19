﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewNess.Helper;
using NewNess.Models;
using NewNess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NewNess.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
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
                    //await _userManager.AddClaimAsync(appUser, new Claim("FullName", appUser.FullName));

                    ModelState.AddModelError("", error.Description);
                }
                return View();

            }
            await _signInManager.SignInAsync(appUser, true);
            await _userManager.AddToRoleAsync(appUser, Helper.Helper.Roles.Member.ToString());
            return RedirectToAction("Index", "Home");
        }
        //        //public async Task<IActionResult> ConfirmEmailAsync(string token, string userid)
        //        //{

        //        //    IdentityUser appUser = _userManager.FindByIdAsync(userid).Result;
        //        //    IdentityResult result = _userManager.
        //        //                ConfirmEmailAsync(appUser, token).Result;
        //        //    if (result.Succeeded)
        //        //    {
        //        //        ViewBag.Message = "Email confirmed successfully!";
        //        //        return View("Success");
        //        //    }
        //        //    else
        //        //    {
        //        //        ViewBag.Message = "Error while confirming your email!";
        //        //        return View("Error");
        //        //    }

        //        //}
        //public IActionResult Error()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult SuccessRegistration()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Appuser user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Userpassword has error");
                return View();
            }
            if (user.IsDeactive)
            {
                ModelState.AddModelError("", "This account has been blocked");
                return View();

            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("UserName", "This account has been blocked for 1min");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username or Userpassword has error");
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        //public async Task CreateRolesAsync()
        //{
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Helper.Roles.Admin.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Helper.Roles.Admin.ToString() });
        //    }
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Helper.Roles.Member.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Helper.Roles.Member.ToString() });
        //    }
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Helper.Roles.Manager.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Helper.Roles.Manager.ToString() });
        //    }
        //}
    }
}
