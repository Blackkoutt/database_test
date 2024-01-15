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

namespace Biblioteka.Pages.Authors
{
    //[Authorize(Roles = "Admin, Employee")]
    public class DetailsModel : PageModel
    {
        private IAuthorRepository _authorRepository;

        public DetailsModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author Author { get; set; } = default!; 

        public IActionResult OnGet(int id)
        {

            var author = _authorRepository.getOne(id);

            if (author == null)
                return NotFound();
            else 
                Author = author;

            return Page();
        }
    }
}
