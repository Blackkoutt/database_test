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

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class EditModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public EditModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var author = _authorRepository.getOne(id);

            if (author == null)
            {
                return NotFound();
            }

            Author = author;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _authorRepository.Update(Author);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.authorId))
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

        private bool AuthorExists(int id)
        {
            var isExisted = _authorRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
