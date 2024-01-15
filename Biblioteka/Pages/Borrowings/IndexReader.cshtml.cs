using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Pages.Borrowings
{
    public class IndexReaderModel : PageModel
    {
        private readonly BibContext _bibContext;
        private readonly UserManager<BibUser> _userManager;
        private IBorrowingRepository _borrowingRepository;

        public IndexReaderModel(BibContext bibContext, UserManager<BibUser> userManager, IBorrowingRepository borrowingRepository)
        {
            _userManager = userManager;
            _bibContext = bibContext;
            _borrowingRepository = borrowingRepository;
        }

    public IList<Borrowing> Borrowing { get; set; } = default!;
        //nowe
        public IList<Reader> Readers { get; set; } = default!;
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = default!;

        public async void OnGet()
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (loggedInUserId != null)
            {
                var user = await _userManager.FindByIdAsync(loggedInUserId);

                if (user != null)
                {
                    string name = user.name;
                    string surname = user.surname;

                    var foundReader = _bibContext.Reader.FirstOrDefault(r => r.name == name && r.surname == surname);

                    if (foundReader != null)
                    {
                        var foundBorrowings = await _bibContext.Borrowing
                            .Include(b => b.book)
                            .ThenInclude(ba => ba.authors)
                            .Where(b => b.readers.Any(rb => rb.readerId == foundReader.readerId))
                            .ToListAsync();

                        if (foundBorrowings != null)
                        {
                            Borrowing = foundBorrowings;
                        }
                        else Borrowing = null;// _borrowingRepository.GetAll();
                    }
                    else Borrowing = null;// _borrowingRepository.GetAll();
                }
            }
           
        }

    }
}
