using Biblioteka.Models;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories
{
    public class ReaderRepository : GenericRepository<Reader>, IReaderRepository
    {
        private readonly BibContext _context;
        public ReaderRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public List<Reader> GetAll()
        {
            return _context.Reader
                .ToList();
        }

        public int GetLastId()
        {
            var id = 1;
            var employees = GetAll();
            if (employees.Any())
                id = employees.Max(e => e.readerId) + 1;
            return id;
        }
    }
}
