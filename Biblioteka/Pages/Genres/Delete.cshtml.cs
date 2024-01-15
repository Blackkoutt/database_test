using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Pages.Genres
{
    public class DeleteModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public DeleteModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [BindProperty]
      public Genre Genre { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            var genre = _genreRepository.getOne(id);

            if (genre == null)
            {
                return NotFound();
            }
            else 
            {
                Genre = genre;
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _genreRepository.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}
