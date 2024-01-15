using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IReader_BorrowingsRepository
    {
        public void Add(Reader_Borrowings reader_borrowings);
        public List<Reader_Borrowings> getAll();
    }
}