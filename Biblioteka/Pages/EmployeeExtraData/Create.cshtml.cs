using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.EmployeeExtraData
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Biblioteka.Context.BibContext _context;
        private IEmployeeDataRepository _employeeDataRepository;

        public CreateModel(IEmployeeDataRepository employeeDataRepository, Biblioteka.Context.BibContext context)
        {
            _employeeDataRepository = employeeDataRepository;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EmployeeData EmployeeData { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost(int id)
        {
            EmployeeData.employeeId = id;
            Employee? employee = _context.Employee.FirstOrDefault(e => e.employeeId == id);

            if(EmployeeData != null && employee != null)
                EmployeeData.employee = employee;

            ModelState.Remove("EmployeeData.employee");

            if (!ModelState.IsValid || _context.EmployeeData == null || EmployeeData == null)
            {
                return Page();
            }

            _employeeDataRepository.Add(EmployeeData);

            return RedirectToPage("../Employees/Index");
        }
    }
}
