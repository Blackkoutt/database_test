using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IReaderRepository
    {
        public List<Reader> getAll();
        public Reader getOne(object id);
        public void Add(Reader entity);
        public void Update(Reader entity);
        public void Delete(object id);
    }
}
