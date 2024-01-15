using Biblioteka.Areas.Identity.Data;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biblioteka.Pages.Basket
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private readonly BibContext _context;
        private IBookRepository _bookRepository;
        private IEmployeeRepository _employeeRepository;
        private IBorrowingRepository _borrowingRepository;

        public IndexModel(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IEmployeeRepository employeeRepository, UserManager<BibUser> userManager, BibContext context)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
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


            List<Employee> employee = _employeeRepository.GetAll();
            Random random = new Random();
            int a = random.Next(0, employee.Count);
            Employee randomEmployee = employee[a];


            // Process the basket data and add to the database
            foreach (var borrowing in basket)
            {

                borrowing.employee = randomEmployee;
                Book book = _bookRepository.getOne(borrowing.book.bookId);
                borrowing.book = book;

                ModelState.Remove("Borrowing.book");
                ModelState.Remove("Borrowing.employee");


                //poprawne dane do bazy
                //wymusza date miesiac pozniej
                borrowing.plannedReturnDate = Borrowing.borrowDate.AddMonths(1);
                //realna data zwrotu na null
                borrowing.returnDate = null;

                //polaczenie readera z borrowingiem
                //if (currentUser == null) currentUser = "1";
                Borrowing.readers.Add(new Reader_Borrowings
                {
                    reader = reader,
                    borrow = borrowing,
                });

                _borrowingRepository.Add(borrowing);
                book.availableCopys -= 1;
                _bookRepository.Update(book);
            }
            return new JsonResult(new { success = true });
        }


    }
}
