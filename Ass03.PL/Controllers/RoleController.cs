using AutoMapper;
using Demo.DAL.Data.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (!string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                   Role = R.Name


                }).ToListAsync();

                return View(Roles);


            }
            else
            {
                var role = await roleManager.FindByNameAsync(SearchValue);

                var MappedRole = new RoleViewModel()
                {
                    
                    Id = role.Id,
                    Role = role.Name

                };

                return View(new List<RoleViewModel>() { MappedRole });
            }
        }



        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var MappedRole = new RoleViewModel()
            {

                Id = role.Id,
                Role = role.Name

            }; ;

            return View(ViewName, MappedRole);

        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return BadRequest();

            return await Details(id, "Edit");


        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {

            if (ModelState.IsValid)
            {

                try
                {

                    var role = await roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Role;
                    await roleManager.UpdateAsync(role);


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
        public async Task<IActionResult> ConfirimedDelete(string id)
        {
            //if (id != employee.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var role = await roleManager.FindByIdAsync(id);
                    await roleManager.DeleteAsync(role);

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
