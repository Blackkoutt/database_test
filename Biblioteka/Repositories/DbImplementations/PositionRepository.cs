using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class PositionRepository : GenericRepository<Room>, IPositionRepository
    {
        private readonly BibContext _context;
        public PositionRepository(BibContext context) : base(context)
        {
            _context = context;
        }
    }

}