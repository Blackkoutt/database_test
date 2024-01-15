using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee")]
    public class ConfirmationPaymentModel : PageModel
    {
        private readonly Biblioteka.Context.BibContext _context;
        private IBorrowingRepository borrowingRepository;
        private readonly UserManager<BibUser> _userManager;

        public ConfirmationPaymentModel(UserManager<BibUser> userManager, Biblioteka.Context.BibContext context, IBorrowingRepository borrowingRepository)
        {
            _context = context;
            this.borrowingRepository = borrowingRepository;
            _userManager = userManager;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        public Reader Reader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_context.Borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.Include(b => b.book).FirstOrDefaultAsync(b => b.borrowId == id);

            if (borrowing == null && borrowing?.book == null)
            {
                return NotFound();
            }
            else
            {
                Borrowing = borrowing;
                var br = await _context.Reader_Borrowings.FirstOrDefaultAsync(r => r.borrowId == borrowing.borrowId);
                if (br != null)
                {
                    var reader = await _context.Reader.FirstOrDefaultAsync(b => b.readerId == br.readerId);
                    if (reader != null)
                        Reader = reader;
                    else
                        return NotFound();
                }
                else
                    return NotFound();

            }
            return Page();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (_context.Borrowing == null)
            {
                return Page();
            }
            else
            {
                var borrowing = await _context.Borrowing.Include(b => b.book).FirstOrDefaultAsync(b => b.borrowId == id);

                if (borrowing != null && borrowing?.book != null)
                {
                    borrowing.IsPaid = true;

                    var userId = _userManager.GetUserId(HttpContext.User);

                    if (userId != null)
                    {
                        var user = await _userManager.FindByIdAsync(userId);

                        if (user != null)
                        {
                            string? email = user.Email;

                            bool isEmployee = await _userManager.IsInRoleAsync(user, "Employee");

                            if (isEmployee)
                            {
                                Employee? employee = await _context.Employee.FirstOrDefaultAsync(e => e.email == email);
                                if (employee != null)
                                {
                                    borrowing.employeeConfirmingPayment = new EmployeeConfirmingPayment() { employee = employee };
                                    _context.EmployeeConfirmingPaymentsBook.Add(borrowing.employeeConfirmingPayment);
                                    Borrowing = borrowing;
                                }
                                else
                                    return NotFound();
                            }
                            else
                                return NotFound();


                        }
                        else
                            return NotFound();
                    }

                    try
                    {
                        _context.Borrowing.Update(Borrowing);

                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BorrowingExists(Borrowing.borrowId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToPage("./IndexAdmin");
                }

                return NotFound();

            }

            bool BorrowingExists(int id)
            {
                var isExisted = borrowingRepository.GetOne(id);

                return isExisted != null ? true : false;
            }
        }
    }
}
