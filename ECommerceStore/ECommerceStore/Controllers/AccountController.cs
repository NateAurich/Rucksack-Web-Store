﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceStore.Models;
using ECommerceStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return View();
            }

            return View();

        }

        // Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName
            };

            var result = await _userManager.CreateAsync(user, rvm.Password);

            if (result.Succeeded)
            {
                return View();
            }

            return View();
        }
    }
}