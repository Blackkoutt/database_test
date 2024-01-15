using Biblioteka.Context;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Models;

namespace Biblioteka.Repositories.DbImplementations
{
                             
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly BibContext _context;
        protected DbSet<T> table;


        // wszystkie funkcje generyczne przetestowane i dzialajace
        public GenericRepository(BibContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public List<T> getAll()
        {
            return table.ToList();
        }

        public T getOne(object id)
        {
            return table.Find(id);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Object not found.", nameof(entity));
            }

            table.Add(entity);
            _context.SaveChanges();
            
        }


        public void Update(T entity)
        {

            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // nie wiadomo czy wszystkie id sa obiektami ale chyba zadziala
        public void Delete(object id)
        {
            //poprzednia wersja: toDelete = getOne(id);
            T toDelete = table.Find(id);
            table.Remove(toDelete);
            _context.SaveChanges();
        }
    }
}
