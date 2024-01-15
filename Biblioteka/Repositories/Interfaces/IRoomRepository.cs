using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IRoomRepository
    {
         public List<Room> getAll();
        public Room getOne(object id);
        public void Add(Room entity);
        public void Update(Room entity);
        public void Delete(object id);
    }
}
