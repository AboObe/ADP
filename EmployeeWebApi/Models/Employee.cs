using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebApi.Models
{
    [Table("employees",Schema ="dbo")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public Guid EmployeeID { get; set; }
        
        [Column("employee_name")]
        public string EmployeeName { get; set; }
        
        [Column("employee_email")]
        public string EmployeeEmail { get; set; }
        
        [Column("employee_phone")]
        public long EmployeePhone { get; set; }
        
        [Column("employee_salaey")]
        public long EmployeeSalary { get; set; }

        [Column("employee_Department")]
        public string EmployeeDepartment { get; set; }
        
    }
}
