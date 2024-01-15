using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        public List<Publisher> getAll();
        public Publisher? getOne(object id);
        public void Add(Publisher publisher);
        public void Delete(object id);
        public void Update(Publisher publisher);
        public Publisher searchPublisher(string publisherName);
    }
}
