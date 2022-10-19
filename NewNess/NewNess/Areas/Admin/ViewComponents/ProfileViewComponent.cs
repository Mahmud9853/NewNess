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

namespace NewNess.Areas.Admin.ViewComponents
{

    [Area("Admin")]
    public class ProfileViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileViewComponent(AppDbContext db, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Appuser users = await _userManager.FindByNameAsync(User.Identity.Name);

            //ProfileVM profileVM = new ProfileVM
            //{
            //        FullName = users.FullName,
            //        Image = users.Image,
                
            //};

            return View(users);


        }

  
    }
}
