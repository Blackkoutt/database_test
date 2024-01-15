using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbRoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly BibContext _context;

        public DbRoomRepository(BibContext context) : base(context)
        {
            _context = context;
        }

    }
}
