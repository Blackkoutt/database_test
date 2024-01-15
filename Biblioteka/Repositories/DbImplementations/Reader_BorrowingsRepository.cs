using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class Reader_BorrowingsRepository : GenericRepository<Reader_Borrowings>, IReader_BorrowingsRepository
    {
        private readonly BibContext _context;
        public Reader_BorrowingsRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public List<Reader_Borrowings> getAll()
        {
            return _context.Reader_Borrowings.ToList();
        }
    }
}
