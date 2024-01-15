using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.BookTypes
{
    public class DeleteModel : PageModel
    {
        private IBookTypeRepository _bookTypeRepository;

        public DeleteModel(IBookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }

        [BindProperty]
      public BookType BookType { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var booktype = _bookTypeRepository.getOne(id);

            if (booktype == null)
            {
                return NotFound();
            }
            else 
            {
                BookType = booktype;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _bookTypeRepository.Delete(id);

            return RedirectToPage("./Index");
        }
    }
}
