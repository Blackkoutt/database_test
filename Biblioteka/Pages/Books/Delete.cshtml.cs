using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Views.Books
{
    public class DeleteModel : PageModel
    {
        private IBookRepository _bookRepository;

        public DeleteModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
      public Book Book { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var book = _bookRepository.getOne(id);

            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
