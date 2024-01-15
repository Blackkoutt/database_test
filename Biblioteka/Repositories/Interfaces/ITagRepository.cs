using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.Interfaces
{
    public interface ITagRepository
    {
        public List<Tag> getAll();
        public Tag? getOne(object id);
        public void Add(Tag tag);
        public void Delete(object id);
        public void Update(Tag tag);
        public Tag searchTag(string tagName);
    }
}
