using Biblioteka.Models;

namespace Biblioteka.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task Add(Employee employee/*, EmployeeData employeeData*/);
        public Task<int> GetLastId();
        public void Delete(object id);
        public void Update(Employee employee);
        public Employee getOne(object id);
        public List<Employee> GetAll();
        public List<Employee> SearchEmployees(string searchTerm);
    }
}