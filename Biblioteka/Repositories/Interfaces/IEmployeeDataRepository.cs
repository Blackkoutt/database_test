using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IEmployeeDataRepository
    {
        public List<EmployeeData> getAll();
        public EmployeeData getOne(object id);
        public void Add(EmployeeData entity);
        public void Update(EmployeeData entity);
        public void Delete(object id);
    }
}