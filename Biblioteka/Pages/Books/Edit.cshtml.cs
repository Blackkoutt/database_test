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
using Biblioteka.Repositories;
using Biblioteka.Context;
using System.Security.Policy;

namespace Biblioteka.Views.Books
{
    public class EditModel : PageModel
    {
        private IBookRepository _bookRepository;
        private BibContext _context;
        public EditModel(IBookRepository bookRepository, BibContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }
        public List<SelectListItem>? Genre { get; set; }
        public List<SelectListItem>? Type { get; set; }
        public List<SelectListItem>? Publisher { get; set; }


        public IActionResult OnGet(int id)
        {
            Genre = _context.Genre.Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
            Type = _context.BookType.Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
            Publisher = _context.Publisher.Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();
            var book = _bookRepository.getOne(id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            return Page();
        }
       [BindProperty]
        public Book Book { get; set; } = default!;
        [BindProperty]
        public string GenreId { get; set; } = default!;
        [BindProperty]
        public string BookTypeId { get; set; } = default!;
        [BindProperty]
        public string PublisherId { get; set; } = default!;

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {

            Genre? foundGenre = _context.Genre.FirstOrDefault(r => r.genreId.ToString().Equals(GenreId.ToString()));

            if (foundGenre != null)
            {
                Book.genre = foundGenre;
            }
            Models.Publisher? foundPublisher = _context.Publisher.FirstOrDefault(r => r.publisherId.ToString().Equals(PublisherId.ToString()));

            if (foundPublisher != null)
            {
                Book.publisher = foundPublisher;
            }
            BookType? foundType = _context.BookType.FirstOrDefault(r => r.typeId.ToString().Equals(BookTypeId.ToString()));

            if (foundType != null)
            {
                Book.type = foundType;
            }
            ModelState.Remove("Book.genre");
            ModelState.Remove("Book.publisher");
            ModelState.Remove("Book.type");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _bookRepository.Update(Book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.bookId))
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

        private bool BookExists(int id)
        {
            var isExisted = _bookRepository.getOne(id);

            return isExisted != null ? true : false;
        }
    }
}
