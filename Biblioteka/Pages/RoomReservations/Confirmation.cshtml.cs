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

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Employee")]
    public class ConfirmationModel : PageModel
    {
        private readonly Biblioteka.Context.BibContext _context;
        private readonly UserManager<BibUser> _userManager;

        public ConfirmationModel(UserManager<BibUser> userManager, Biblioteka.Context.BibContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public RoomReservation RoomReservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_context.RoomReservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.RoomReservation.FirstOrDefaultAsync(b => b.reservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                RoomReservation = reservation;

            }
            return Page();

        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (_context.RoomReservation == null)
            {
                return Page();
            }
            else
            {
                var roomReservation = await _context.RoomReservation.FirstOrDefaultAsync(b => b.reservationId == id);

                if (roomReservation != null)
                {

                    roomReservation.Confirmation = true;

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
                                    roomReservation.employee = employee;
                                    RoomReservation = roomReservation;
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
                    else
                        return NotFound();

                    ModelState.Remove("Borrowing.employee");
                    ModelState.Remove("Borrowing.book");



                    try
                    {
                        _context.RoomReservation.Update(RoomReservation);

                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReservationExists(RoomReservation.reservationId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToPage("./IndexEmployees");
                }
            }

            return NotFound();

        }

        bool ReservationExists(int id)
        {
            var isExisted = _context.RoomReservation.FirstOrDefault(r => r.reservationId == id);

            return isExisted != null ? true : false;
        }
    }
}
