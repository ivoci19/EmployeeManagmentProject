using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public interface IAudit
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }  
        DateTime UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }
}
