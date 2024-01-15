namespace Biblioteka.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public List<T> getAll();
        public T getOne(object id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(object id);
    }
}
