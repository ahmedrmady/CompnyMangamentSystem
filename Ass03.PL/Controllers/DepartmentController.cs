using Demo.BLL.Interfaces;
using Demo.BLL.Repostioris;
using Demo.DAL.Data.Models;
using Demo.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    //[ValidateAntiForgeryToken]
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DeparmentRepositry.GetAll();

            var departmentsVM =  _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(departmentsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DeparmentRepositry.Add(department);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                    TempData["Messege"] = "The Department Created Succesfuly!";
                else
                    TempData["Messege"] = "Error Occured !..The Department not Created!";


                return RedirectToAction(nameof(Index));
            }

            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DeparmentRepositry.Get(id.Value);
            if (department is null)
                return NotFound();

            var departmentVM = _mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, departmentVM);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            return await Details(id.Value, "Edit");


        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            //if (id !=department.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DeparmentRepositry.Update(department);
                    _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(departmentVM);


        }

        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id is null)
                return BadRequest();

            return  await Details(id.Value, "Delete");


        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            //if (id != departmentVM.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _unitOfWork.DeparmentRepositry.Delete(department);
                   await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(departmentVM);


        }


    }
}
