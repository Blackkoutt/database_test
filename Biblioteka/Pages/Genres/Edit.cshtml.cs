using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Genres
{
    public class EditModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public EditModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [BindProperty]
        public Genre Genre { get; set; } = default!;

        public IActionResult OnGet(int id)
        {          

            var genre =  _genreRepository.getOne(id);
            if (genre == null)
            {
                return NotFound();
            }
            Genre = genre;
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
                _genreRepository.Update(Genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(Genre.genreId))
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

        private bool GenreExists(int id)
        {
            var isExisted = _genreRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
