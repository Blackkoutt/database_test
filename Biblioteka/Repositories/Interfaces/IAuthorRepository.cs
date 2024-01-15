using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public Author getOne(object id);
        public void Update(Author autor);
        public void Delete(object id);
        public List<Author> getAll();

        public void Add(Author author);
    }
}