using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Genres
{
    public class CreateModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public CreateModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genre Genre { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid ||  Genre == null)
            {
                return Page();
            }

            _genreRepository.Add(Genre);

            return RedirectToPage("./Index");
        }
    }
}
