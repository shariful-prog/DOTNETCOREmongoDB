using MongoDB.Driver;
using ShadhinMongoDB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadhinMongoDB
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeService(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);

        }

        public async Task<IQueryable<Employee>> Get()
        {
            IQueryable<Employee> employees;
            employees = (IQueryable<Employee>)await _employees.Find(emp => true).ToListAsync();
            return employees;
        }

        public Employee Get(string id) =>
            _employees.Find(emp => emp.Id == id).FirstOrDefault();

    }
}
