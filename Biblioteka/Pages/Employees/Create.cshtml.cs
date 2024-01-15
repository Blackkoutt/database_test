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
using NuGet.LibraryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Employees
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private IEmployeeRepository _employeeRepository;
        private Biblioteka.Context.BibContext _bibContext;
       

        public CreateModel(IEmployeeRepository employeeRepository, Biblioteka.Context.BibContext bibContext)
        {
            _employeeRepository = employeeRepository;
            _bibContext = bibContext;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;
        public List<SelectListItem>? Positions { get; set; }

        [BindProperty]
        public string PositionId { get; set; } = default!;


        public IActionResult OnGet()
        {
            Positions = _bibContext.Position.Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name } ).ToList();
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            Position? foundPosition = _bibContext.Position.FirstOrDefault(p => p.positionId.ToString().Equals(PositionId.ToString()));

            if(foundPosition != null && Employee != null)
                Employee.position = foundPosition;
            else
                ModelState.AddModelError("", "Stanowisko jest wymagane.");

            ModelState.Remove("Employee.position");
            ModelState.Remove("Employee.employeeData");

            if (Employee == null || !ModelState.IsValid)
             {
                 Positions = _bibContext.Position.Select(p => new SelectListItem { Value = p.positionId.ToString(), Text = p.name }).ToList();
                 return Page();
             }    

            //_employeeRepository.Add(Employee);
            
            return RedirectToPage("./Index");
        }
    }
}
