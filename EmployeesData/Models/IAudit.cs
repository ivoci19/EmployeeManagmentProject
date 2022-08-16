using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Models
{
    public interface IAudit
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }  
        DateTime UpdatedDate { get; set; }
        int UpdatedBy { get; set; }
        bool IsActive { get; set; }
    }
}
