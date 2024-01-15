using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;
using Biblioteka.Context;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;

namespace Biblioteka.Views.Books
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IBookRepository _bookRepository;
        private IBookOpinionRepository _bookOpinionRepository;
        private BibContext _context;

        public DetailsModel(BibContext context, UserManager<BibUser> userManager, IBookRepository bookRepository, IBookOpinionRepository bookOpinionRepository)
        {
            _context = context;
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookOpinionRepository = bookOpinionRepository;

        }
        [BindProperty]
        public List<Book_Opinions> Opinions { get; set; } = default!;

        [BindProperty]
        public Book_Opinions User_Opinion { get; set; } = default!;
        [BindProperty]
        public Book Book { get; set; } 
        public bool IsRatingAdded { get; set; }
        public BibUser user;

        public IActionResult OnGet(int id)
        {
            var book = _bookRepository.getOne(id);
            Opinions = _bookOpinionRepository.getOpinionsForBook(book);

            Reader reader;

            if (User.IsInRole("Reader") && !User.IsInRole("Employee"))
            {
                reader = _context.Reader.FirstOrDefault(r => r.email == User.Identity.Name);

                IsRatingAdded = Opinions.ToList().Where(o => (o.readerId == reader.readerId) && (o.bookId == book.bookId)).Any();
            }
            else
            {
                IsRatingAdded = true;
            }

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
            ModelState.Remove("Book.ISBN");
            ModelState.Remove("Book.genre");
            ModelState.Remove("Book.publisher");
            ModelState.Remove("Book.type");
            ModelState.Remove("Book.title");
            ModelState.Remove("Book.description");
            ModelState.Remove("User_Opinion.reader");
            ModelState.Remove("User_Opinion.book");
            var book = _bookRepository.getOne(id);
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Model error: {error.ErrorMessage}");
                    }

                }
                return OnGet(id);
            }

            if (User.IsInRole("Reader") && !User.IsInRole("Employee"))
            {
                User_Opinion.reader = _context.Reader.FirstOrDefault(r => r.email == User.Identity.Name);
            }
            
            
            User_Opinion.book = book;

            var opinionsCount = _bookOpinionRepository.getOpinionsForBook(book).Count;

            if(book.ratingAVG == null)
            {
                book.ratingAVG = Math.Round(User_Opinion.rating,2);
            }
            else
            {
                double? newRating = ((book.ratingAVG * opinionsCount) + User_Opinion.rating) / (opinionsCount + 1);
                int a = 1;
                book.ratingAVG = Math.Round((double)newRating, 2);
                int c = 1;
            }

            User_Opinion.addedDate = DateTime.Now;


            var opinion = User_Opinion;
            var booknew = book;

            _bookRepository.Update(book);
            _bookOpinionRepository.Add(User_Opinion);


            return RedirectToPage("./Details", new { id = book.bookId });
        }
    }
}
