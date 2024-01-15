using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Views.Books
{
    public class IndexModel : PageModel
    {
        private IBookRepository _bookRepository;

        public IndexModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Book> Book { get; set; }

        public void OnGet()
        {
            Book = _bookRepository.SearchBooks(SearchTerm);
        }

       /* public void OnPost(int bookId)
        {
            Book book = _bookRepository.getOne(bookId);
            if (book.availableCopys < 1)
            {
                TempData["ErrorMessage"] = "No copies available for borrowing.";
                Page();
            }

            RedirectToPage("../Books/Create");
        }*/
    }
}
