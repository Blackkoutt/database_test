using Biblioteka.Areas.Identity.Data;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Pages.Basket
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private readonly BibContext _context;
        private IBookRepository _bookRepository;
        private IEmployeeRepository _employeeRepository;
        private IBorrowingRepository _borrowingRepository;
        private IReader_BorrowingsRepository _reader_BorrowingsRepository;

        public CreateModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IEmployeeRepository employeeRepository, IReader_BorrowingsRepository reader_BorrowingsRepository, UserManager<BibUser> userManager, BibContext context)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _reader_BorrowingsRepository = reader_BorrowingsRepository;
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostFinalizeAsync([FromBody] List<Borrowing> basket)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);

            Reader reader = _context.Reader.FirstOrDefault(r => r.email == user.Email);

            var userBorrowingCount = _context.Reader_Borrowings
            .Count(borrowing => borrowing.readerId == reader.readerId);

            List<Employee> employee = _employeeRepository.GetAll();
            Random random = new Random();
            int a = random.Next(0, employee.Count);
            Employee randomEmployee = employee[a];


            var leftBorrowings = 5 - userBorrowingCount;
            foreach (var borrowing in basket)
            {
                if (leftBorrowings > 0)
                {
                    borrowing.employee = randomEmployee;
                    Book book = _bookRepository.getOne(borrowing.book.bookId);
                    borrowing.book = book;

                    ModelState.Remove("Borrowing.book");
                    ModelState.Remove("Borrowing.employee");


                    //poprawne dane do bazy
                    //wymusza date miesiac pozniej
                    borrowing.plannedReturnDate = borrowing.borrowDate.AddMonths(1);
                    //realna data zwrotu na null
                    borrowing.returnDate = null;

                    //polaczenie readera z borrowingiem
                    //if (currentUser == null) currentUser = "1";
                    borrowing.readers.Add(new Reader_Borrowings
                    {
                        reader = reader,
                        borrow = borrowing,
                    });

                    _borrowingRepository.Add(borrowing);
                    book.availableCopys -= 1;
                    _bookRepository.Update(book);

                    leftBorrowings -= 1;
                } else
                {
                    return new JsonResult(new { success = false });
                }
                
            }
            return new JsonResult(new { success = true });
        }


    }
}
