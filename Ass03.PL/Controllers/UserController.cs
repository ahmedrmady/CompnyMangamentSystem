using AutoMapper;
using Demo.DAL.Data.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (!string.IsNullOrEmpty(SearchValue))
            {
                var Users = await userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    Email = U.Email,
                    FName = U.FName,
                    LName = U.LName,
                    PhoneNumber = U.PhoneNumber,
                    Roles = userManager.GetRolesAsync(U).Result,


                }).ToListAsync();

                return View(Users);


            }
            else
            {
                var user = await userManager.FindByEmailAsync(SearchValue);

                var MappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FName = user.FName,
                    LName = user.LName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = userManager.GetRolesAsync(user).Result,
                };

                return View(new List<UserViewModel>() { MappedUser });
            }
        }



        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            var userVM = mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(ViewName, userVM);

        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return BadRequest();

            return await Details(id , "Edit");


        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    var user = mapper.Map<UserViewModel, ApplicationUser>(model);

                    await userManager.UpdateAsync(user);


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(model);


        }


        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest();


            return await Details(id: id, "Delete");


        }
        [HttpPost]
        public async Task<IActionResult> ConfirimedDelete(string id )
        {
            //if (id != employee.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var user = await userManager.FindByIdAsync(id);
                  await  userManager.DeleteAsync(user);
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(id);


        }
        #endregion




    }
}
