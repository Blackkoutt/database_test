using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbGenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private BibContext _context;

        public DbGenreRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public Genre searchGenre(string genreName)
        {
            return _context.Genre.FirstOrDefault(g => g.name.ToLower().Contains(genreName.ToLower()));
        }
    }
}
