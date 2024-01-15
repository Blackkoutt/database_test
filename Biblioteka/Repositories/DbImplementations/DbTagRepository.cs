using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbTagRepository : GenericRepository<Tag>, ITagRepository
    {
        private BibContext _context;

        public DbTagRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public Tag searchTag(string tagName)
        {
            return _context.Tag.FirstOrDefault(t => t.name.ToLower().Contains(tagName.ToLower()));
        }

    }
}
