using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.OracleClient;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class CreateModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public CreateModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid || Author == null)
            {
                return Page();
            }

            _authorRepository.Add(Author);

            return RedirectToPage("./Index");
        }
    }
}
