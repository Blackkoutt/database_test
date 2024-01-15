using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Admin, Employee, Reader")]
    public class IndexEmployeesModel : PageModel
    {
        private readonly IRoomReservationRepository _roomReservationRepository;

        public IndexEmployeesModel(IRoomReservationRepository roomReservationRepository)
        {
            _roomReservationRepository = roomReservationRepository;
        }

        public IList<RoomReservation> RoomReservation { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
           
                RoomReservation = _roomReservationRepository.SearchRoomReservations(SearchTerm);
            
        }
    }
}
