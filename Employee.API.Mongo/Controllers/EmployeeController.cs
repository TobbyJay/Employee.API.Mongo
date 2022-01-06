using Microsoft.AspNetCore.Mvc;

using Employee.API.Mongo.Services;
using Employees = Employee.API.Mongo.Model.Employee;


namespace Employee.API.Mongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeesService _service;
        public EmployeeController(EmployeesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Employees>> Get()
        {
            return await _service.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Employees>> Get(string id)
        {
            var Employees = await _service.GetAsync(id);

            if (Employees is null)  return NotFound();
            
            return Employees;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employees newEmployees)
        {
            await _service.CreateAsync(newEmployees);

            return CreatedAtAction(nameof(Get), new { id = newEmployees.Id }, newEmployees);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Employees updatedEmployees)
        {
            var Employees = await _service.GetAsync(id);

            if (Employees is null)  return NotFound();
            
            updatedEmployees.Id = Employees.Id;

            await _service.UpdateAsync(id, updatedEmployees);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Employees = await _service.GetAsync(id);

            if (Employees is null)  return NotFound();
            
            await _service.RemoveAsync(Employees.Id!);

            return NoContent();
        }
    }
}