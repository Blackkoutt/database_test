using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IBookTypeRepository
    {
        public List<BookType> getAll();
        public BookType? getOne(object id);
        public void Add(BookType type);
        public void Delete(object id);
        public void Update(BookType type);
        public BookType searchBookType(string booktypeName);
    }
}
