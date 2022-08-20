using System;

namespace EmployeesData.Models
{
    public interface IAudit
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
        bool IsActive { get; set; }
    }
}
