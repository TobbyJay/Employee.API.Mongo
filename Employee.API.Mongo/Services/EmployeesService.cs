using Microsoft.Extensions.Options;

using MongoDB.Driver;

using Employee.API.Mongo.Model;
using Employees = Employee.API.Mongo.Model.Employee;

namespace Employee.API.Mongo.Services
{
    public class EmployeesService
    {
        private readonly IMongoCollection<Employees> _EmployeessCollection;

        public EmployeesService(
            IOptions<EmployeeDbSettings> EmployeesStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EmployeesStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EmployeesStoreDatabaseSettings.Value.DatabaseName);

            _EmployeessCollection = mongoDatabase.GetCollection<Employees>(
                EmployeesStoreDatabaseSettings.Value.EmployeesCollectionName);
        }

        public async Task<List<Employees>> GetAsync()
        {
            return await _EmployeessCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Employees?> GetAsync(string id)
        {
            return await _EmployeessCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Employees newEmployees)
        {
            await _EmployeessCollection.InsertOneAsync(newEmployees);
        }

        public async Task UpdateAsync(string id, Employees updatedEmployees)
        {
            await _EmployeessCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployees);
        }

        public async Task RemoveAsync(string id)
        {
            await _EmployeessCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}