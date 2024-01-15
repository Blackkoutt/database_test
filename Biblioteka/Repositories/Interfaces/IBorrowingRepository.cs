using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBorrowingRepository
    {
        public void Add(Borrowing borrowing);
        public void Delete(object id);
        public Borrowing GetOne(int id);
        public List<Borrowing> GetAll();
        public IList<Borrowing> SearchBorrowings(string searchTerm);
    }
}
