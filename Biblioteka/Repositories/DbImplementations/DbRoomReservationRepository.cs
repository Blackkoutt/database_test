using Microsoft.EntityFrameworkCore;
using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using System.Security.Policy;
using System.Globalization;

namespace Biblioteka.Repositories.DbImplementations

{
    public class DbRoomReservationRepository : GenericRepository<RoomReservation>, IRoomReservationRepository
    {
        private readonly BibContext _context;

        public DbRoomReservationRepository(BibContext context) : base(context)
        {
            _context = context;
        }

        public List<RoomReservation> getAll()
        {
            return _context.RoomReservation
                .Include(re => re.reader)
                .Include(ro => ro.room)
                .Include(e => e.employee)
                .ToList();
        }

        public RoomReservation getOne(int reservationId)
        {
            return _context.RoomReservation
                .Include(re => re.reader)
                .Include(ro => ro.room)
                .Include(e => e.employee)
                .FirstOrDefault(r => r.reservationId == reservationId);
        }

        public List<RoomReservation> SearchRoomReservations(string searchTerm)
        {
            var query = _context.RoomReservation
                .Include(re => re.reader)
                .Include(ro => ro.room)
                .Include(e => e.employee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTerms = searchTerm.Split(',').Select(term => term.Trim().ToLower()).ToArray();

                // Pobierz dane z bazy danych do pamięci
                var reservationsInMemory = query.ToList();

                var filteredReservations = reservationsInMemory
                    .Where(reservation =>
                        searchTerms.All(searchTerm =>
                            (reservation.reader.FullName != null && reservation.reader.FullName.ToLower().Contains(searchTerm)) ||
                            (DateTime.TryParseExact(searchTerm, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                             (reservation.begginingOfReservationDate.Date == date.Date || reservation.endOfReservationDate.Date == date.Date))
                        )
                    )
                    .AsQueryable();

                // Zaktualizuj oryginalne zapytanie
                query = filteredReservations.AsQueryable();
            }

            return query.ToList();
        }

    }
}
