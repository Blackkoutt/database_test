using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.DbImplementations
{
    public class EmployeeDataRepository : GenericRepository<EmployeeData>, IEmployeeDataRepository
    {
        private readonly BibContext _context;
        public EmployeeDataRepository(BibContext context) : base(context)
        {
            _context = context;
        }

    }
}