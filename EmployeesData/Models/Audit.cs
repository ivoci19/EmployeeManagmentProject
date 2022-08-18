using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public class Audit : IAudit
    {
       public string CreatedBy { get; set; }
       public DateTime CreatedDate { get; set; }
       public DateTime UpdatedDate { get; set; }
       public string UpdatedBy { get; set; }
       public bool IsActive { get; set; }
    }
}
