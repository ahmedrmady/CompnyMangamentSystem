using Demo.BLL.Interfaces;
using Demo.BLL.Repostioris;
using Demo.DAL.Data.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchInp)
        {
            var employees = Enumerable.Empty<Employee>();

            if (!string.IsNullOrEmpty(searchInp))
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInp.ToLower());
            else
             employees = await _unitOfWork.EmployeeRepository.GetAll();


            var empsVM = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(empsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            employeeVM.ImageName =  await DocumentSetting.UploadFile(employeeVM.Image, "images");
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Add(employee);

                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =  await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();
            var employeeVM = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, employeeVM);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            return  await Details(id.Value, "Edit");


        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
 
            if (ModelState.IsValid)
            {

                try
                {
                    var oldEmployee = await _unitOfWork.EmployeeRepository.Get(employeeVM.Id);
                   
                    if(!string.IsNullOrEmpty(oldEmployee.ImageName))
                        DocumentSetting.DeleteFile(oldEmployee.ImageName, "images");
                       employeeVM.ImageName=await DocumentSetting.UploadFile(employeeVM.Image, "images");
                    
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(employee);
                    await _unitOfWork.Complete();


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(employeeVM);


        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();


            return await Details(id:id.Value, "Delete");


        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            //if (id != employee.Id)
            //    return BadRequest();
            if (ModelState.IsValid)
            {

                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(employee);
                    var count = await _unitOfWork.Complete();
                    if (count > 0)
                        DocumentSetting.DeleteFile(employee.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    throw;
                }
            }

            return View(employeeVM);


        }


    }
}
