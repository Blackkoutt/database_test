using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Admin,Reader")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IReader_BorrowingsRepository _readerBorrowingsRepository;
        private IBorrowingRepository _borrowingRepository;
        private IBookRepository _bookRepository;
        private IEmployeeRepository _employeeRepository;
        private IReaderRepository _readerRepository;
        private readonly Biblioteka.Context.BibContext _context;

        //private BibUser user;
        private Borrowing[] basket;


        public CreateModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IEmployeeRepository employeeRepository, IReaderRepository readerRepository, UserManager<BibUser> userManager, BibContext context, IReader_BorrowingsRepository readerBorrowingsRepository)
        {
            _readerBorrowingsRepository = readerBorrowingsRepository;
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _employeeRepository = employeeRepository;
            _readerRepository = readerRepository;
            _userManager = userManager;
            //this.user = user;
            _context = context;
        }
        //[BindProperty]
        public Reader_Borrowings Reader_Borrowings { get; set; } = new Reader_Borrowings();
        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        public Book Book { get; set; } = default!;
        [BindProperty]
        public int Id { get; set; }


        public IActionResult OnGet(int id)
        {
            Id = id;
            Book = _bookRepository.getOne(id);
            return Page();
        }

        
    }
}