using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.DbImplementations;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbPublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private BibContext _context;

        public DbPublisherRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        
        public Publisher searchPublisher(string publisherName)
        {
            return _context.Publisher.FirstOrDefault(p => p.name.ToLower().Contains(publisherName.ToLower()));
        }
    }
}
