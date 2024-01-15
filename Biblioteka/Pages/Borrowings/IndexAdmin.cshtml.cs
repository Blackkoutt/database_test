using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka.Pages.Borrowings
{
    [Authorize(Roles = "Employee, Admin")]
    public class IndexAdminModel : PageModel
    {
        private IBorrowingRepository _borrowingRepository;
        private IReaderRepository _readerRepository;
        private Biblioteka.Context.BibContext _bibContext;

        public IndexAdminModel(IBorrowingRepository borrowingRepository, IReaderRepository readerRepository, Biblioteka.Context.BibContext bibContext)
        {
            _borrowingRepository = borrowingRepository;
            _readerRepository = readerRepository;
            _bibContext = bibContext;
        }

        public IList<Borrowing> Borrowing { get; set; } = default!;

        public IList<Reader> Readers { get; set; } = default!;
        public IList<Reader_Borrowings> Reader_Borrowings { get; set; } = default!;
        public string SearchTerm { get; set; }

        public void OnGet(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Borrowing = _borrowingRepository.SearchBorrowings(searchTerm);
            }
            else
            {
                Borrowing = _borrowingRepository.GetAll();
            }

            Readers = _readerRepository.getAll();
            Reader_Borrowings = _bibContext.Reader_Borrowings.ToList();
        }
    }
}
