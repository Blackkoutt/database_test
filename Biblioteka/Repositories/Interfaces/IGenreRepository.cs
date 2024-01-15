using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        public List<Genre> getAll();
        public Genre? getOne(object id);
        public void Add(Genre genre);
        public void Delete(object id);
        public void Update(Genre genre);
        public Genre searchGenre(string genreName);
    }
}
