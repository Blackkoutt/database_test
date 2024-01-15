using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.EmployeeExtraData
{
    //[Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IEmployeeDataRepository _employeeDataRepository;
        private Biblioteka.Context.BibContext _context;

        public EditModel(IEmployeeDataRepository employeeDataRepository, Biblioteka.Context.BibContext context)
        {
            _employeeDataRepository = employeeDataRepository;
            _context = context;
        }


        [BindProperty]
        public EmployeeData EmployeeData { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var employeedata =  _employeeDataRepository.getOne(id);
            if (employeedata == null)
            {
                return NotFound();
            }

            EmployeeData = employeedata;
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(int id)
        {
            EmployeeData.employeeId = id;
            Employee? employee = _context.Employee.FirstOrDefault(e => e.employeeId == id);

            if (employee != null)
                EmployeeData.employee = employee;

            ModelState.Remove("EmployeeData.employee");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _employeeDataRepository.Update(EmployeeData);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDataExists(EmployeeData.employeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Employees/Index");
        }

        private bool EmployeeDataExists(int id)
        {
            var isExisted = _employeeDataRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
