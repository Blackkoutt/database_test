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
using Biblioteka.Repositories.DbImplementations;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Employees
{
    //[Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private Biblioteka.Context.BibContext _bibContext;

        public EditModel(IEmployeeRepository employeeRepository, Biblioteka.Context.BibContext bibContext)
        {
            _employeeRepository = employeeRepository;
            _bibContext = bibContext;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public List<SelectListItem>? Positions { get; set; }

        [BindProperty]
        public string PositionId { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            Positions = _bibContext.Position.Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();

            var employee = _employeeRepository.getOne(id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            Position? foundPosition = _bibContext.Position.FirstOrDefault(p => p.positionId.ToString().Equals(PositionId.ToString()));

            if(foundPosition != null && Employee != null)
                Employee.position = foundPosition;
            else
                ModelState.AddModelError("", "Stanowisko jest wymagane.");

            ModelState.Remove("Employee.position");
            ModelState.Remove("Employee.employeeData");

            if (!ModelState.IsValid || Employee == null)
            {
                Positions = _bibContext.Position.Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();

                return Page();
            }

            try
            {
                _employeeRepository.Update(Employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.employeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
            var isExisted = _employeeRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
