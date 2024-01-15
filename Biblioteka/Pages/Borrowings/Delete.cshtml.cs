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

namespace Biblioteka.Pages.Borrowings
{
    //[Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private IBookRepository _bookRepository;

        public DeleteModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        [BindProperty]
        public int Id {  get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnGet(int id)
        {

            var borrowing = _borrowingRepository.GetOne(id);
            Book = borrowing.book;
           

            if (borrowing == null)
            {
                return NotFound();
            }
            else 
            {
                Borrowing = borrowing;
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var borrowing = _borrowingRepository.GetOne(id);
            Book book = borrowing.book;
            
            //Book book = _bookRepository.getOne(Id);
            

            _borrowingRepository.Delete(id);
            // po usunieciu wypozyczenia przywroc liczbe dostepnych ksiazek o 1
            book.availableCopys += 1;
            _bookRepository.Update(book);
            return RedirectToPage("./IndexReader");
        }
    }
}
