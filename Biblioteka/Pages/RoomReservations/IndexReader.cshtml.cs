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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Reader")]
    public class IndexReaderModel : PageModel
    {
        private Biblioteka.Context.BibContext _context;
        private readonly UserManager<BibUser> _userManager;

        public IndexReaderModel(BibContext bibContext, UserManager<BibUser> userManager)
        {
            _userManager = userManager;
            _context = bibContext;
        }

        public IList<RoomReservation> RoomReservation { get; set; } = default!;

        public async void OnGet()
        {

            if (User.IsInRole("Reader"))
            {
                RoomReservation = await _context.RoomReservation
                .Include(re => re.reader)
                .Include(ro => ro.room)
                .Include(e => e.employee)
                .Where(rr => rr.reader.email == User.Identity.Name)
                .ToListAsync();
            }
        }
    }
}
