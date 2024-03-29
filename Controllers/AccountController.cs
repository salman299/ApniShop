﻿using ApniShop.Models;
using ApniShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApniShop.Utalities;


namespace ApniShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;


        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager) {

            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
  
        }
        
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result=await _signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe, false);
 
                if (result.Succeeded) {

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(" ","Invalid Login attempt.");

            }
            return View();
        }

        public async Task<IActionResult> Register()
        {

            if (!_roleManager.RoleExistsAsync(Helper.User).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.User));

            }
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                };

                var result = await _userManager.CreateAsync(user,model.Password);

                if (result.Succeeded) {

                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index","Home");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }




    }
}
