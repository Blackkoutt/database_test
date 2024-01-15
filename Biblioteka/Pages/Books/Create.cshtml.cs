using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder.Extensions;

namespace Biblioteka.Views.Books
{
    public class CreateModel : PageModel
    {
        private IBookRepository _bookRepository;
        private BibContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ILogger<CreateModel> logger, IBookRepository bookRepository, BibContext bibcontext)
        {
            _bookRepository = bookRepository;
            _context = bibcontext;
            _logger = logger;
        }
        public List<SelectListItem>? Genre { get; set; }
        public List<SelectListItem>? Type { get; set; }
        public List<SelectListItem>? Publisher { get; set; }
        public List<SelectListItem>? Author { get; set; }
        public List<SelectListItem>? Tag { get; set; }
        public IActionResult OnGet()
        {
            Genre = _context.Genre.Select(r => new SelectListItem { Value = r.genreId.ToString(), Text = r.name }).ToList();
            Type = _context.BookType.Select(r => new SelectListItem { Value = r.typeId.ToString(), Text = r.name }).ToList();
            Publisher = _context.Publisher.Select(r => new SelectListItem { Value = r.publisherId.ToString(), Text = r.name }).ToList();

            Author = _context.Author.Select(r => new SelectListItem { Value = r.authorId.ToString(), Text = r.name + " " + r.surname }).ToList();
            Tag = _context.Tag.Select(t => new SelectListItem { Value = t.tagId.ToString(), Text = t.name }).ToList();

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;
        [BindProperty]
        public string GenreId { get; set; } = default!;
        [BindProperty]
        public string[] TagIds { get; set; } = default!;
        [BindProperty]
        public string BookTypeId { get; set; } = default!;

        [BindProperty]
        public string PublisherId { get; set; } = default!;

        [BindProperty]
        public string[] AuthorIds { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {


            Genre? foundGenre = _context.Genre.FirstOrDefault(r => r.genreId.ToString().Equals(GenreId.ToString()));

            if (foundGenre != null)
            {
                Book.genre = foundGenre;
            }

            Book.tags = new List<Book_Tag>();

            foreach (var tagId in TagIds)
            {
                Tag? foundTag = _context.Tag.FirstOrDefault(r => r.tagId.ToString() == tagId);
                if (foundTag != null)
                {
                    Book.tags.Add(new Book_Tag { tag = foundTag, book = Book });
                }
            }


            Publisher? foundPublisher = _context.Publisher.FirstOrDefault(r => r.publisherId.ToString().Equals(PublisherId.ToString()));

            if (foundPublisher != null)
            {
                Book.publisher = foundPublisher;
            }
            BookType? foundType = _context.BookType.FirstOrDefault(r => r.typeId.ToString().Equals(BookTypeId.ToString()));

            if (foundType != null)
            {
                Book.type = foundType;
            }

            Book.authors = new List<Book_Author>();

            if (AuthorIds != null && AuthorIds.Length > 0)
            {
                foreach (var authorId in AuthorIds)
                {
                    Author? foundAuthor = _context.Author.FirstOrDefault(r => r.authorId.ToString() == authorId);
                    if (foundAuthor != null)
                    {
                        // Add to the existing authors list
                        Book.authors.Add(new Book_Author { author = foundAuthor, book = Book });
                    }
                }
            }





            ModelState.Remove("Book.genre");
            ModelState.Remove("Book.authors");
            ModelState.Remove("Book.publisher");
            ModelState.Remove("Book.type");
            if (!ModelState.IsValid || Book == null)
            {
                return Page();
            }

            _bookRepository.Add(Book);

            return RedirectToPage("./Index");
        }
    }
}