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
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Biblioteka.Areas.Identity.Data;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Reader")]
    public class CreateModel : PageModel
    {

        private Biblioteka.Context.BibContext _bibContext;
        private readonly UserManager<BibUser> _userManager;

        public CreateModel(Biblioteka.Context.BibContext bibContext, UserManager<BibUser> userManager)
        {
            _bibContext = bibContext;
            _userManager = userManager;
        }

        public List<SelectListItem>? Rooms { get; set; }
        public IActionResult OnGet()
        {
            Rooms = _bibContext.Room.Select(r => new SelectListItem { Value = r.roomNumber.ToString(), Text = r.FullData }).ToList();
            //Employees = _bibContext.Employee.Select(r => new SelectListItem { Value = r.employeeId.ToString(), Text = r.FullName }).ToList();

            return Page();
        }

        [BindProperty]
        public RoomReservation RoomReservation { get; set; } = default!;

        [BindProperty]
        public string RoomId { get; set; } = default!;

        public IActionResult OnPost()
        {

            Reader? foundReader = _bibContext.Reader.FirstOrDefault(r => r.email == User.Identity.Name);
            if (foundReader != null && RoomReservation != null)
                RoomReservation.reader = foundReader;
            else
                ModelState.AddModelError("", "Wymagana jest osoba składająca rezerwację");


            Room? foundRoom = _bibContext.Room.FirstOrDefault(r => r.roomNumber.ToString().Equals(RoomId.ToString()));

            if (foundRoom != null && RoomReservation != null)
            {
                RoomReservation.room = foundRoom;
            }
          

            RoomReservation.Confirmation = false;

            if (IsTerminWrong(RoomReservation))
                ModelState.AddModelError(" ", "Data początku rezerwacji nie może być później niż data końca rezerwacji lub daty nie mogą być takie same.");

            if (IsReserved(RoomReservation))
                ModelState.AddModelError("RoomId", "Już ktoś zarezerwował salę w obrębie tego terminu.");

            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            if (RoomReservation != null && DateTime.Compare(RoomReservation.begginingOfReservationDate, now) < 0)
                ModelState.AddModelError(" ", "Nie można zarezerwować daty początku rezerwacji na dzień, który już był.");

            if (RoomReservation != null && DateTime.Compare(RoomReservation.endOfReservationDate, now) < 0)
                ModelState.AddModelError(" ", "Nie można zarezerwować daty końca rezerwacji na dzień, który już był.");

            ModelState.Remove("RoomReservation.reader");
            ModelState.Remove("RoomReservation.room");

            if (!ModelState.IsValid || RoomReservation == null || foundReader == null)
            {
                Rooms = _bibContext.Room.Select(r => new SelectListItem { Value = r.roomNumber.ToString(), Text = r.FullData }).ToList();

                if (foundRoom == null)
                    ModelState.AddModelError("", "Należy wybrać salę.");

                return Page();
            }

            _bibContext.RoomReservation.Add(RoomReservation);
            _bibContext.SaveChanges();

            return RedirectToPage("./IndexReader");
        }

        private bool IsTerminWrong(RoomReservation reservation)
        {
            if (reservation != null)
                return (DateTime.Compare(reservation.begginingOfReservationDate, reservation.endOfReservationDate) >= 0);

            return false;
        }

        private bool IsReserved(RoomReservation toSaveReservation)
        {
            if (toSaveReservation != null)
            {
                foreach (RoomReservation savedReservation in _bibContext.RoomReservation)
                {

                    if (savedReservation != null && savedReservation.room != null &&
                        savedReservation.room.roomNumber == toSaveReservation.room.roomNumber &&
                        IsTerminIncluded(toSaveReservation, savedReservation.begginingOfReservationDate, savedReservation.endOfReservationDate)
                       )
                    {
                        return true;
                    }
                }

                return false;
            }
            return false;
        }

        private bool IsTerminIncluded(RoomReservation toSaveReservation, DateTime beginningDateRange, DateTime endDateRange)
        {
            if (toSaveReservation != null)
            {
                DateTime startingDate = toSaveReservation.begginingOfReservationDate;
                DateTime endDate = toSaveReservation.endOfReservationDate;

                if (DateTime.Compare(startingDate, beginningDateRange) >= 0 && DateTime.Compare(startingDate, endDateRange) <= 0)
                    return true;
                else if (DateTime.Compare(endDate, beginningDateRange) >= 0 && DateTime.Compare(endDate, endDateRange) <= 0)
                    return true;
                else if(DateTime.Compare(startingDate, beginningDateRange)<0 && DateTime.Compare(endDate, endDateRange) > 0)
                    return true;
                else
                    return false;
            }

            return false;
        }

    }
}