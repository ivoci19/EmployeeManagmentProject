using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels
{
    public class ProjectBaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
    public class ProjectViewModel : ProjectBaseViewModel
    {
        public int Id { get; set; }

    }
    public class ProjectEditViewModel : ProjectBaseViewModel
    {
    }
}
