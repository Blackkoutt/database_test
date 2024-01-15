using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IRoomReservationRepository
    {
        public void Delete(object id);
        public List<RoomReservation> getAll();
        public RoomReservation getOne(int reservationId);
        public List<RoomReservation> SearchRoomReservations(string searchTerm);
    }
}
