using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.Employees
{
   // [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private Biblioteka.Context.BibContext _bibContext;

        public DeleteModel(IEmployeeRepository employeeRepository, BibContext bibContext)
        {
            _employeeRepository = employeeRepository;
            _bibContext = bibContext;   
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var employee = _employeeRepository.getOne(id);

            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Employee? employee = _employeeRepository.getOne(id);

            if(employee != null)
            {
                var user = _bibContext.Users.FirstOrDefault(u => u.name == employee.name && u.surname == employee.surname && u.Email == employee.email);

                if(user != null) 
                {
                    _bibContext.Users.Remove(user); 
                }
            }
            
            _employeeRepository.Delete(id);

            return RedirectToPage("./Index");
        }
    }
}
