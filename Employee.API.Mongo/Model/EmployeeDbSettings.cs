namespace Employee.API.Mongo.Model
{
    public class EmployeeDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EmployeesCollectionName { get; set; } = null!;
    }
}
