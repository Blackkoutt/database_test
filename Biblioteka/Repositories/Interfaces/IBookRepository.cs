using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBookRepository
    {
       
        public Book getOne(int id);
        public Book getOne(string title);
        public List<Book> getAll();
        public void Add(Book book);
        public void Delete(object id);
        public void Update(Book book);
        public IEnumerable<Book> SearchBooks(string searchTerm);
    }
}