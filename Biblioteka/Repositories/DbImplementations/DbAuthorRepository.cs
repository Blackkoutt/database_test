using Biblioteka.Models;
using Biblioteka.Context;
using Microsoft.Data.SqlClient;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.DbImplementations;
namespace Biblioteka.Repositories

{
    public class DbAuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        private readonly BibContext _context;
        public DbAuthorRepository(BibContext context): base(context)
        {
            _context = context;
        }

        
    }
}
