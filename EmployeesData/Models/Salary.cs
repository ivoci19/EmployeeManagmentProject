using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
     public class Salary
    {
        public int Id { get; set; }
        public DateTime SalaryDate { get; set; }
        public string SalaryDescripition { get; set; }
        public string SalaryMonth { get; set; } 
        public int UserId { get; set;}
        public User User { get; set; }
    }
}
