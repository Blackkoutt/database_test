using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.BookTypes
{
    public class CreateModel : PageModel
    {
        private IBookTypeRepository _bookTypeRepository;

        public CreateModel(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BookType BookType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid || BookType == null)
            {
                return Page();
            }

            _bookTypeRepository.Add(BookType);

            return RedirectToPage("./Index");
        }
    }
}
