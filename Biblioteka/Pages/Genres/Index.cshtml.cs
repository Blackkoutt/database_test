using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private IGenreRepository _genreRepository;

        public IndexModel(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IList<Genre> Genre { get;set; } = default!;

        public void OnGet(string? genreName)
        {
            if (!string.IsNullOrEmpty(genreName))
            {
                Genre = new List<Genre> { _genreRepository.searchGenre(genreName) };
            }
            else
            {
                // Jeśli nazwa nie jest określona, pobierz wszystkie wydawnictwa
                Genre = _genreRepository.getAll();
            }
        }
    }
}
