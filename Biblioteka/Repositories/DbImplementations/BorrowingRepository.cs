using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Biblioteka.Repositories
{
    public class BorrowingRepository : GenericRepository<Borrowing>, IBorrowingRepository
    {
        private readonly BibContext _context;
        public BorrowingRepository(BibContext context) : base(context)
        {
            _context = context;
        }


        public Borrowing GetOne(int id)
        {
            return _context.Borrowing
                .Include(b => b.book)
                .ThenInclude(ba => ba.authors)
                .FirstOrDefault(m => m.borrowId == id);
        }

        public List<Borrowing> GetAll()
        {
            return _context.Borrowing
                .Include(b => b.book)
                .ThenInclude(ba => ba.authors)
                .ToList();
        }

        public IList<Borrowing> SearchBorrowings(string searchTerm)
        {
            var query = _context.Borrowing
                .Include(b => b.book)
                .Include(b => b.readers)
                .ThenInclude(r => r.reader)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTerms = searchTerm.Split(',').Select(term => term.Trim().ToLower()).ToArray();

                // Pobierz dane z bazy danych do pamięci
                var borrowingsInMemory = query.ToList();

                var filteredBorrowings = borrowingsInMemory
                    .Where(borrowing =>
                        searchTerms.All(searchTerm =>
                            (borrowing.book.title != null && borrowing.book.title.ToLower().Contains(searchTerm)) ||
                            borrowing.readers.Any(r =>
                                (r.reader.name + " " + r.reader.surname).ToLower().Contains(searchTerm)) ||
                            (DateTime.TryParseExact(searchTerm, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                             borrowing.borrowDate.Date == date.Date))
                    )
                    .AsQueryable();

                // Zaktualizuj oryginalne zapytanie
                query = filteredBorrowings.AsQueryable();
            }

            return query.ToList();
        }


    }
}
