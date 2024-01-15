using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbBookTypeRepository : GenericRepository<BookType>, IBookTypeRepository
    {
        private BibContext _context;

        public DbBookTypeRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public BookType searchBookType(string booktypeName)
        {
            return _context.BookType.FirstOrDefault(b => b.name.ToLower().Contains(booktypeName.ToLower()));
        }

    }
}
