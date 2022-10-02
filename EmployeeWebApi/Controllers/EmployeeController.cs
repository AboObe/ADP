using EmployeeWebApi.DBContext;
using EmployeeWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            return employeeDbContext.Employees;
        }

        [HttpGet("{employeeId:Guid}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid employeeId)
        {
            var employee = await employeeDbContext.Employees.FindAsync(employeeId);
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            employee.EmployeeID = Guid.NewGuid();
            await employeeDbContext.Employees.AddAsync(employee);
            await employeeDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Employee employee)
        {
            employeeDbContext.Employees.Update(employee);
            await employeeDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{employeeId:Guid}")]
        public async Task<ActionResult> Delete(Guid employeeId)
        {
            var employee = await employeeDbContext.Employees.FindAsync(employeeId);
            employeeDbContext.Employees.Remove(employee);
            await employeeDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
